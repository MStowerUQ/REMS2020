﻿using MediatR;
using Rems.Application.Common;
using Rems.Application.Common.Extensions;
using Rems.Application.CQRS;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rems.Infrastructure.Excel
{
    public class ExcelImporter : ProgressTracker
    {
        public DataSet Data { get; set; }

        public override int Items => Data.Tables.Count;
        public override int Steps => Data.Tables.Cast<DataTable>().Sum(d => d.Rows.Count);

        public event RequestItem ItemNotFound;

        public ExcelImporter(QueryHandler query, CommandHandler command) : base(query, command)
        { }        

        public async override Task Run()
        {
            try
            {
                if (!OnSendQuery(new ConnectionExists()))
                    throw new Exception("No existing database connection");

                foreach (DataTable table in Data.Tables)
                    await InsertTable(table);

                OnTaskFinished();
            }
            catch (Exception e)
            {
                OnTaskFailed(e);
            }      
        }

        /// <summary>
        /// Adds the given data table to the context
        /// </summary>
        private Task InsertTable(DataTable table)
        {
            // Skip the empty / notes tables
            if (table.TableName == "Notes" || table.Rows.Count < 1)
                return Task.Run(() => Unit.Value);

            OnNextItem(table.TableName);

            var type = table.ExtendedProperties["Type"] as Type;

            IRequest command;
            switch (table.TableName)
            {
                case "Design":
                    OnSendCommand(new InsertDesignsCommand() { Table = table }).Wait();
                    command = new InsertPlotsCommand() { Table = table };
                    break;

                case "PlotData":
                    command = new InsertPlotDataTableCommand()
                    {
                        Table = table,
                        Skip = 4,
                        Type = "Crop"
                    };
                    break;

                case "MetData":
                    command = new InsertMetDataTableCommand()
                    {
                        Table = table,
                        Skip = 2,
                        Type = "Climate"
                    };
                    break;

                case "SoilLayerData":
                    command = new InsertSoilLayerTableCommand()
                    {
                        Table = table,
                        Skip = 5,
                        Type = "SoilLayer"
                    };
                    break;

                case "Irrigation":
                case "Fertilization":
                    command = new InsertOperationsTableCommand() 
                    { 
                        Table = table, 
                        Type = type,
                        ItemNotFound = ItemNotFound
                    };
                    break;

                case "Soils":
                case "SoilLayer":
                    var query = new EntityTypeQuery() 
                    { 
                        Name = type.Name + "Trait"
                    };
                    var dependency = OnSendQuery(query);

                    command = new InsertTraitTableCommand()
                    {
                        Table = table,
                        Type = type,
                        Dependency = dependency,
                        ItemNotFound = ItemNotFound
                    };
                    break;

                default:
                    command = new InsertTableCommand()
                    { 
                        Table = table,
                        Type = type,
                        ItemNotFound = ItemNotFound
                    };                    
                    break;
            }
            return OnSendCommand(command);
        }

    }
}