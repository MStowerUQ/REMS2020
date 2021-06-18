﻿using Rems.Application.Common;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace WindowsClient.Models
{
    public class TableNode : DataNode<ExcelTable, DataTable>
    {
        public GroupNode Required = new GroupNode("Required")
        {
            Advice = new("These nodes describe the columns that REMS expects to find in the spreadsheet" +
                "and are required for the import."),
            ImageKey = "Properties",
            SelectedImageKey = "Properties"
        };

        public GroupNode Traits = new GroupNode("Traits")
        {
            Advice = new("Any additional columns in the spreadsheet will be imported as trait data."),
            ImageKey = "Traits",
            SelectedImageKey = "Traits"
        };

        public override string Key 
        {
            get
            {
                if (Excel.Ignore)
                    return "ExcelOff";
                else if (!Excel.Valid)
                    return "Warning";
                else
                    return "Excel";                
            }
        }

        public TableNode(ExcelTable excel) : base(excel)
        {
            Nodes.Add(Required);
            Nodes.Add(Traits);

            Traits.Items.Add(new ToolStripMenuItem("Add all as Traits", null, AddTraits));
            Traits.Items.Add(new ToolStripMenuItem("Ignore all", null, ToggleIgnoreAll));
        }

        protected override void OnMenuOpening(object sender, EventArgs args)
        {
            // No configuration required
        }

        /// <summary>
        /// Adds a trait to the database for every invalid child node
        /// </summary>
        public async void AddTraits(object sender, EventArgs args)
        {
            var nodes = TreeView.SelectedNode.Nodes.OfType<TraitNode>();
            foreach (var node in nodes)
                if (!node.Excel.Valid)
                    await node.AddTrait();

            Refresh();
        }

        /// <summary>
        /// Sets the ignored state of all child nodes
        /// </summary>
        private void ToggleIgnoreAll(object sender, EventArgs args)
        {
            var nodes = TreeView.SelectedNode.Nodes.OfType<DataNode<ExcelColumn, DataColumn>>();
            foreach (var node in nodes)
                node.ToggleIgnore();

            Refresh();
        }

        public override void Refresh()
        {
            Excel.Valid = true;

            var nodes = Nodes.OfType<GroupNode>()
                .SelectMany(g => g.Nodes.OfType<DataNode<ExcelColumn, DataColumn>>())
                .ToArray();
            
            foreach (var node in nodes)
            {
                node.Refresh();

                if (!node.Excel.Ignore)
                    Excel.Valid &= node.Excel.Valid;
            }

            CheckNode(Required);
            CheckNode(Traits);

            ImageKey = SelectedImageKey = Key;
        }

        private void CheckNode(GroupNode node)
        {
            if (node.Nodes.Count == 0)
                Nodes.Remove(node);
            else if (!Nodes.Contains(node))
                Nodes.Add(node);
        }
    }
}
