﻿using MediatR;
using Rems.Application.CQRS;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsClient.Models;

namespace WindowsClient.Controls
{
    /// <summary>
    /// Manages the presentation of experiment data
    /// </summary>
    public partial class ExperimentDetailer : UserControl
    {
        public ExperimentDetailer()
        {
            InitializeComponent();          
            
            experimentsTree.AfterSelect += OnNodeSelected;
            Load += async (s, e) => await LoadTreeView(); 
        }

        /// <summary>
        /// Update the experiments tree view
        /// </summary>
        public async Task LoadTreeView()
        {
            experimentsTree.Nodes.Clear();

            var exps = await QueryManager.Request(new ExperimentsQuery());

            foreach (var exp in exps)
                experimentsTree.Nodes.Add(new ExperimentNode(exp.Value) { ID = exp.Key });

            experimentsTree.SelectedNode = experimentsTree.TopNode;
            experimentsTree.Refresh();
        }

        /// <summary>
        /// Calls a function dependent on the type of node selected in the tree
        /// </summary>
        private async void OnNodeSelected(object sender, EventArgs e)
        {
            var node = experimentsTree.SelectedNode;

            try
            {
                if (node.Parent is ExperimentNode exp)
                    AttachControl(await exp.GetSelectedControl());
                else if (node is ExperimentNode en)
                {
                    var summary = new ExperimentSummariser();
                    AttachControl(summary);
                    await summary.GetSummary(en.ID);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void AttachControl(Control control)
        {
            container.Panel2.Controls.Clear();
            container.Panel2.Controls.Add(control);
            control.Dock = DockStyle.Fill;            
        }
    }
}
