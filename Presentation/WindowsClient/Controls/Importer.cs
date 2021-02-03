﻿using ExcelDataReader;
using MediatR;
using Microsoft.EntityFrameworkCore.Internal;

using Rems.Application.Common.Extensions;
using Rems.Application.CQRS;
using Rems.Infrastructure.Excel;

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using WindowsClient.Models;

namespace WindowsClient.Controls
{    
    /// <summary>
    /// Enables the import of excel data
    /// </summary>
    public partial class Importer : UserControl
    {
        /// <summary>
        /// Occurs when data is requested from the mediator
        /// </summary>
        public event Func<object, Task<object>> Query;

        /// <summary>
        /// Occurs after a file is imported
        /// </summary>
        public event Action FileImported;

        /// <summary>
        /// Occurs when the current import stage has changed
        /// </summary>
        public event Action<Stage> StageChanged;

        /// <summary>
        /// Occurs when the file to import from has changed
        /// </summary>
        public event Action<string> FileChanged;

        private async Task<T> InvokeQuery<T>(IRequest<T> query)
            => (T)await Query(query);

        /// <summary>
        /// The system folder most recently accessed by the user
        /// </summary>
        public string Folder { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        /// <summary>
        /// Collection of icons used by the data tree
        /// </summary>
        private ImageList images;

        public Importer() : base()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;

            // Add icons to the image list
            images = new ImageList();
            images.Images.Add("ValidOff", Properties.Resources.ValidOff);
            images.Images.Add("InvalidOff", Properties.Resources.InvalidOff);
            images.Images.Add("ValidOn", Properties.Resources.ValidOn);
            images.Images.Add("InvalidOn", Properties.Resources.InvalidOn);
            images.Images.Add("WarningOn", Properties.Resources.WarningOn);
            images.Images.Add("WarningOff", Properties.Resources.WarningOff);
            images.Images.Add("Add", Properties.Resources.Add);
            images.ImageSize = new System.Drawing.Size(14, 14);

            dataTree.ImageList = images;
            
            // Force right click to select node
            dataTree.NodeMouseClick += (s, a) => dataTree.SelectedNode = dataTree.GetNodeAt(a.X, a.Y);

            tracker.TaskBegun += RunImporter;
        }        

        #region Data
        /// <summary>
        /// The excel data
        /// </summary>
        public DataSet Data { get; set; } 

        /// <summary>
        /// Reads the data from the given file
        /// </summary>
        /// <remarks>
        /// It is assumed that the file is either of .xls or .xlsx format
        /// </remarks>
        private DataSet ReadData(string filepath)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using (var stream = File.Open(filepath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    return reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });
                }
            }
        }

        /// <summary>
        /// Attempts to sanitise raw excel data so it can be read into the database
        /// </summary>        
        private async Task CleanData(DataSet data)
        {
            dataTree.Nodes.Clear();

            foreach (var table in data.Tables.Cast<DataTable>().ToArray())
            {   
                if (table.TableName == "Notes" || table.Rows.Count == 0)
                {
                    data.Tables.Remove(table);
                    continue;
                }

                // TODO: This is a quick workaround, find better way to handle factors/levels table
                if (table.TableName == "Factors") table.TableName = "Levels";

                // TODO: This is a quick workaround, find better way to handle planting/sowing table
                if (table.TableName == "Planting") table.TableName = "Sowing";

                // Remove any duplicate rows from the table
                table.RemoveDuplicateRows();
                
                var type = await InvokeQuery(new EntityTypeQuery() { Name = table.TableName });
                if (type == null) throw new Exception("Cannot import unrecognised table: " + table.TableName);

                table.ExtendedProperties.Add("Type", type);
                
                dataTree.Nodes.Add(await ValidateTable(table));
            }            
        }

        /// <summary>
        /// Validate the contents of a table
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private async Task<TreeNode> ValidateTable(DataTable table)
        {
            var xt = new ExcelTable(table);
            var tnode = new DataNode(xt);
            tnode.Query += (o) => Query?.Invoke(o);

            bool valid = true;
            // Prepare individual columns for import
            foreach (var col in table.Columns.Cast<DataColumn>().ToArray())
            {
                // Remove empty columns
                if (col.ColumnName.Contains("Column"))
                {
                    table.Columns.Remove(col);
                    continue;
                }                

                // Create a node for the column
                var xc = new ExcelColumn(col);
                xc.Query += (o) => Query?.Invoke(o);

                var cnode = new DataNode(xc);
                cnode.Query += (o) => Query?.Invoke(o);

                await xc.Validate();

                tnode.Nodes.Add(cnode);

                // The table is only valid if all the columns are valid
                valid &= (bool)col.ExtendedProperties["Valid"];
            }

            // If the table node is not valid for import, update the state to warn the user
            if (valid)
                tnode.UpdateState("Valid", true);     
            else
                tnode.UpdateState("Override", "Warning");

            var validater = CreateTableValidater(tnode);
            validater.Validate();

            return tnode;
        }

        public static IValidater CreateTableValidater(DataNode table)
        {
            switch (table.Text)
            {
                case "Design":
                    return new DesignValidater(table);

                case "HarvestData":
                case "PlotData":
                    return new DataValidater(table);

                case "MetData":
                    return new MetValidater(table);

                case "SoilLayerData":
                    return new SoilLayerValidater(table);

                case "Irrigation":
                case "Fertilization":
                case "Tillage":
                    return new OperationsValidater(table);

                case "Soils":
                case "SoilLayer":
                case "SoilLayers":
                    return new TableValidater(table);

                default:
                    return new TableValidater(table);
            }
        }

        

        #endregion
        /// <summary>
        /// Handles the selection of a new node in the tree
        /// </summary>
        private void TreeAfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node is DataNode node)
            {
                importData.DataSource = node.Excel.Source;
                importData.Format();

                columnLabel.Text = node.Text;
                
                adviceBox.Clear();
                adviceBox.AddText(node.Advice);
            }
        }

        /// <summary>
        /// Lets the user select a file to open for import
        /// </summary>
        /// <returns>True if the file is valid, false otherwise</returns>
        public async Task<bool> OpenFile()
        {
            using (var open = new OpenFileDialog())
            {
                open.InitialDirectory = Folder;
                open.Filter = "Excel Files (2007) (*.xlsx;*.xls)|*.xlsx;*.xls";

                if (open.ShowDialog() != DialogResult.OK) return false;

                Folder = Path.GetDirectoryName(open.FileName);

                try
                {                    
                    Data = ReadData(open.FileName);
                    await CleanData(Data);

                    fileBox.Text = Path.GetFileName(open.FileName);

                    dataTree.SelectedNode = dataTree.TopNode;

                    StageChanged?.Invoke(Stage.Validation);
                    FileChanged?.Invoke(open.FileName);

                    return true;
                }
                catch (IOException error)
                {
                    MessageBox.Show(error.Message);
                    return false;
                }                
            }
        }

        /// <summary>
        /// Runs the excel importer
        /// </summary>
        private async void RunImporter()
        {
            try
            {
                bool connected = await InvokeQuery(new ConnectionExists());
                if (!connected)
                {
                    MessageBox.Show("A database must be opened or created before importing");
                    return;
                }

                if (Data is null)
                {
                    MessageBox.Show("There is no loaded data to import. Please load and validate data.");
                    return;
                }

                var states = dataTree.Nodes.Cast<TreeNode>()
                    .Select(n => n.Tag as DataTable)
                    .Where(t => t.ExtendedProperties["Valid"] is false);

                if (states.Any())
                {
                    MessageBox.Show("There errors in the data preventing import.");
                    return;
                }

                var importer = new ExcelImporter { Data = Data };
                importer.Query += (o) => Query?.Invoke(o);

                tracker.SetSteps(importer);

                importer.NextItem += tracker.OnNextTask;
                importer.IncrementProgress += tracker.OnProgressChanged;
                importer.TaskFinished += FileImported;
                importer.TaskFailed += tracker.OnTaskFailed;

                await importer.Run();

                tracker.Reset();

                StageChanged.Invoke(Stage.Imported);
            }
            catch (Exception error)
            {
                while (error.InnerException != null) error = error.InnerException;
                MessageBox.Show(error.Message);
            }
        }        

        /// <summary>
        /// Handles the file button click event
        /// </summary>
        private async void OnFileButtonClicked(object sender, EventArgs e) => await OpenFile();
    }
}
