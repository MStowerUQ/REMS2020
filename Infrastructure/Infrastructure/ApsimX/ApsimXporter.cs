﻿using MediatR;
using Models.Core;
using Models.Core.ApsimFile;
using Models.PMF;
using Models.Storage;
using Rems.Application.Common;
using Rems.Application.Common.Extensions;
using Rems.Application.CQRS;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Rems.Infrastructure.ApsimX
{
    public class ApsimXporter : ProgressTracker
    {
        public Simulations Simulations { get; set; }

        public override int Items { get; protected set; } = 0;
        public override int Steps { get; protected set; } = 0;

        public ApsimXporter(string filename, QueryHandler handler)
        {
            Simulations = new Simulations
            {
                FileName = filename
            };
            SendQuery += handler;

            Items = OnSendQuery(new ExperimentCount());
            Steps = Items * 28;
        }

        public async override Task Run()
        {
            await Task.Run(InitialiseSimulations);

            var exps = OnSendQuery(new ExperimentsQuery());
            var validations = new Folder() { Name = "Validations" };

            validations.AddCombinedResults(NextNode.Sibling)
               .AddPanel(NextNode.None);
            Simulations.Add(validations, NextNode.None);

            var experiments = new Folder() { Name = "Experiments" };
            foreach (var exp in exps)
            {
                await AddExperiment(experiments, exp.Key, exp.Value);
            }

            Simulations.Add(experiments, NextNode.None);

            File.WriteAllText(Simulations.FileName, FileFormat.WriteToString(Simulations));

            OnTaskFinished();
        }

        private void InitialiseSimulations()
        {
            var dVars = EventManager.OnRequestRawData("Daily.txt").Split('\n');
            var dEvents = new string[] { "[Clock].DoReport" };

            var hVars = EventManager.OnRequestRawData("Harvest.txt").Split('\n');
            var hEvents = new string[] { "[Sorghum].Harvesting" };

            Simulations
                .Add<DataStore>(NextNode.Child)
                    .AddExcelInput(NextNode.Sibling, "Observed")
                    .AddExcelInput(NextNode.Sibling, "DailyObserved")
                    .AddPredictedObserved(NextNode.Sibling, "Harvest")
                    .AddPredictedObserved(NextNode.Parent, "Daily")
                .Add<Replacements>(NextNode.Child, "Replacements")
                    .AddPlant(NextNode.Sibling)
                    .AddReport(NextNode.Sibling, "Daily", dVars, dEvents)
                    .AddReport(NextNode.Parent, "Harvest", hVars, hEvents);
        }

        private Task AddExperiment(Model model, int id, string name)
        {
            OnNextItem(name);
            return Task.Run(() => model.AddExperiment(NextNode.None, id, name));
        }
    }

    public static class Extensions
    {
        public static IModel AddPlant(this IModel model, NextNode next)
        {
            var sorghumModel = Path.Combine("DataFiles", "apsimx", "Sorghum.json");
            var sorghum = JsonTools.LoadJson<Plant>(sorghumModel);

            return model.Add(sorghum, next);
        }
    }
}
