﻿namespace WindowsClient.Controls
{
    partial class Importer
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.nodeSplitter = new System.Windows.Forms.SplitContainer();
            this.ignoreBox = new System.Windows.Forms.CheckBox();
            this.isTraitBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.propertiesBox = new System.Windows.Forms.ComboBox();
            this.importData = new System.Windows.Forms.DataGridView();
            this.columnLabel = new System.Windows.Forms.Label();
            this.stateBox = new System.Windows.Forms.PictureBox();
            this.dataTree = new System.Windows.Forms.TreeView();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.fileBox = new System.Windows.Forms.TextBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.tabcontrol = new System.Windows.Forms.TabControl();
            this.pageImport = new System.Windows.Forms.TabPage();
            this.pageExport = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.nodeSplitter)).BeginInit();
            this.nodeSplitter.Panel1.SuspendLayout();
            this.nodeSplitter.Panel2.SuspendLayout();
            this.nodeSplitter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.importData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stateBox)).BeginInit();
            this.tabcontrol.SuspendLayout();
            this.pageImport.SuspendLayout();
            this.pageExport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // nodeSplitter
            // 
            this.nodeSplitter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nodeSplitter.Location = new System.Drawing.Point(3, 27);
            this.nodeSplitter.Name = "nodeSplitter";
            // 
            // nodeSplitter.Panel1
            // 
            this.nodeSplitter.Panel1.Controls.Add(this.ignoreBox);
            // 
            // nodeSplitter.Panel2
            // 
            this.nodeSplitter.Panel2.Controls.Add(this.isTraitBox);
            this.nodeSplitter.Panel2.Controls.Add(this.label2);
            this.nodeSplitter.Panel2.Controls.Add(this.propertiesBox);
            this.nodeSplitter.Size = new System.Drawing.Size(485, 26);
            this.nodeSplitter.SplitterDistance = 66;
            this.nodeSplitter.TabIndex = 9;
            // 
            // ignoreBox
            // 
            this.ignoreBox.AutoSize = true;
            this.ignoreBox.Location = new System.Drawing.Point(4, 4);
            this.ignoreBox.Name = "ignoreBox";
            this.ignoreBox.Size = new System.Drawing.Size(59, 17);
            this.ignoreBox.TabIndex = 0;
            this.ignoreBox.Text = "Ignore ";
            this.ignoreBox.UseVisualStyleBackColor = true;
            this.ignoreBox.CheckedChanged += new System.EventHandler(this.IgnoreBoxCheckChanged);
            // 
            // isTraitBox
            // 
            this.isTraitBox.AutoSize = true;
            this.isTraitBox.Location = new System.Drawing.Point(3, 4);
            this.isTraitBox.Name = "isTraitBox";
            this.isTraitBox.Size = new System.Drawing.Size(58, 17);
            this.isTraitBox.TabIndex = 2;
            this.isTraitBox.Text = "Is Trait";
            this.isTraitBox.UseVisualStyleBackColor = true;
            this.isTraitBox.CheckedChanged += new System.EventHandler(this.IsTraitBoxCheckChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(143, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Property:";
            // 
            // propertiesBox
            // 
            this.propertiesBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.propertiesBox.Enabled = false;
            this.propertiesBox.FormattingEnabled = true;
            this.propertiesBox.Location = new System.Drawing.Point(192, 2);
            this.propertiesBox.Name = "propertiesBox";
            this.propertiesBox.Size = new System.Drawing.Size(223, 21);
            this.propertiesBox.TabIndex = 1;
            this.propertiesBox.SelectedIndexChanged += new System.EventHandler(this.PropertiesSelectionChanged);
            // 
            // importData
            // 
            this.importData.AllowUserToAddRows = false;
            this.importData.AllowUserToDeleteRows = false;
            this.importData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.importData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.importData.Location = new System.Drawing.Point(3, 56);
            this.importData.Name = "importData";
            this.importData.Size = new System.Drawing.Size(485, 529);
            this.importData.TabIndex = 0;
            // 
            // columnLabel
            // 
            this.columnLabel.AutoSize = true;
            this.columnLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.columnLabel.Location = new System.Drawing.Point(30, 29);
            this.columnLabel.Name = "columnLabel";
            this.columnLabel.Size = new System.Drawing.Size(44, 17);
            this.columnLabel.TabIndex = 3;
            this.columnLabel.Text = "         ";
            // 
            // stateBox
            // 
            this.stateBox.Location = new System.Drawing.Point(4, 29);
            this.stateBox.Name = "stateBox";
            this.stateBox.Size = new System.Drawing.Size(16, 16);
            this.stateBox.TabIndex = 2;
            this.stateBox.TabStop = false;
            // 
            // dataTree
            // 
            this.dataTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataTree.HideSelection = false;
            this.dataTree.Location = new System.Drawing.Point(0, 49);
            this.dataTree.Name = "dataTree";
            this.dataTree.Size = new System.Drawing.Size(148, 505);
            this.dataTree.TabIndex = 8;
            this.dataTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeAfterSelect);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(3, 3);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(142, 23);
            this.btnImport.TabIndex = 7;
            this.btnImport.Text = "Run";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.OnImportClicked);
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoad.Location = new System.Drawing.Point(413, 1);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 6;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.OnLoadClicked);
            // 
            // fileBox
            // 
            this.fileBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fileBox.Location = new System.Drawing.Point(3, 3);
            this.fileBox.Name = "fileBox";
            this.fileBox.ReadOnly = true;
            this.fileBox.Size = new System.Drawing.Size(404, 20);
            this.fileBox.TabIndex = 10;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(3, 3);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(142, 23);
            this.btnExport.TabIndex = 11;
            this.btnExport.Text = "Run";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.OnExportClick);
            // 
            // tabcontrol
            // 
            this.tabcontrol.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tabcontrol.Controls.Add(this.pageImport);
            this.tabcontrol.Controls.Add(this.pageExport);
            this.tabcontrol.ItemSize = new System.Drawing.Size(76, 20);
            this.tabcontrol.Location = new System.Drawing.Point(3, 3);
            this.tabcontrol.Name = "tabcontrol";
            this.tabcontrol.SelectedIndex = 0;
            this.tabcontrol.Size = new System.Drawing.Size(156, 582);
            this.tabcontrol.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabcontrol.TabIndex = 12;
            // 
            // pageImport
            // 
            this.pageImport.Controls.Add(this.dataTree);
            this.pageImport.Controls.Add(this.btnImport);
            this.pageImport.Controls.Add(this.columnLabel);
            this.pageImport.Controls.Add(this.stateBox);
            this.pageImport.Location = new System.Drawing.Point(4, 24);
            this.pageImport.Name = "pageImport";
            this.pageImport.Padding = new System.Windows.Forms.Padding(3);
            this.pageImport.Size = new System.Drawing.Size(148, 554);
            this.pageImport.TabIndex = 0;
            this.pageImport.Text = "Import";
            this.pageImport.UseVisualStyleBackColor = true;
            // 
            // pageExport
            // 
            this.pageExport.Controls.Add(this.btnExport);
            this.pageExport.Location = new System.Drawing.Point(4, 24);
            this.pageExport.Name = "pageExport";
            this.pageExport.Padding = new System.Windows.Forms.Padding(3);
            this.pageExport.Size = new System.Drawing.Size(148, 553);
            this.pageExport.TabIndex = 1;
            this.pageExport.Text = "Export";
            this.pageExport.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabcontrol);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.importData);
            this.splitContainer1.Panel2.Controls.Add(this.nodeSplitter);
            this.splitContainer1.Panel2.Controls.Add(this.btnLoad);
            this.splitContainer1.Panel2.Controls.Add(this.fileBox);
            this.splitContainer1.Size = new System.Drawing.Size(655, 588);
            this.splitContainer1.SplitterDistance = 160;
            this.splitContainer1.TabIndex = 13;
            // 
            // Importer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "Importer";
            this.Size = new System.Drawing.Size(655, 588);
            this.nodeSplitter.Panel1.ResumeLayout(false);
            this.nodeSplitter.Panel1.PerformLayout();
            this.nodeSplitter.Panel2.ResumeLayout(false);
            this.nodeSplitter.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nodeSplitter)).EndInit();
            this.nodeSplitter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.importData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stateBox)).EndInit();
            this.tabcontrol.ResumeLayout(false);
            this.pageImport.ResumeLayout(false);
            this.pageImport.PerformLayout();
            this.pageExport.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer nodeSplitter;
        private System.Windows.Forms.DataGridView importData;
        private System.Windows.Forms.TreeView dataTree;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.ComboBox propertiesBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox stateBox;
        private System.Windows.Forms.Label columnLabel;
        private System.Windows.Forms.TextBox fileBox;
        private System.Windows.Forms.CheckBox ignoreBox;
        private System.Windows.Forms.CheckBox isTraitBox;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.TabControl tabcontrol;
        private System.Windows.Forms.TabPage pageImport;
        private System.Windows.Forms.TabPage pageExport;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}
