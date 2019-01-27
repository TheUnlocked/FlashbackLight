﻿namespace FlashbackLight
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.currentSPCEntryList = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.stxViewer = new System.Windows.Forms.GroupBox();
            this.currentSTXStringList = new System.Windows.Forms.DataGridView();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wrdViewer = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmdMoveDown = new System.Windows.Forms.Button();
            this.cmdMoveUp = new System.Windows.Forms.Button();
            this.cmdDelete = new System.Windows.Forms.Button();
            this.cmdAdd = new System.Windows.Forms.Button();
            this.currentWRDCommandList = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.argCountBox = new System.Windows.Forms.NumericUpDown();
            this.opcodeComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.currentWRDHexEditor = new Be.Windows.Forms.HexBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.stxViewer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.currentSTXStringList)).BeginInit();
            this.wrdViewer.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.argCountBox)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // currentSPCEntryList
            // 
            this.currentSPCEntryList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.currentSPCEntryList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentSPCEntryList.FormattingEnabled = true;
            this.currentSPCEntryList.ItemHeight = 16;
            this.currentSPCEntryList.Location = new System.Drawing.Point(3, 16);
            this.currentSPCEntryList.Name = "currentSPCEntryList";
            this.currentSPCEntryList.Size = new System.Drawing.Size(200, 418);
            this.currentSPCEntryList.TabIndex = 0;
            this.currentSPCEntryList.DoubleClick += new System.EventHandler(this.CurrentSPCEntryList_DoubleClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(752, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.openToolStripMenuItem.Text = "Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Enabled = false;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.Save);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Enabled = false;
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.SaveAs);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.MinimumSize = new System.Drawing.Size(600, 437);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.stxViewer);
            this.splitContainer1.Panel2.Controls.Add(this.wrdViewer);
            this.splitContainer1.Size = new System.Drawing.Size(752, 437);
            this.splitContainer1.SplitterDistance = 206;
            this.splitContainer1.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.currentSPCEntryList);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(206, 437);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SPC Browser";
            // 
            // stxViewer
            // 
            this.stxViewer.Controls.Add(this.currentSTXStringList);
            this.stxViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stxViewer.Location = new System.Drawing.Point(0, 0);
            this.stxViewer.Name = "stxViewer";
            this.stxViewer.Size = new System.Drawing.Size(542, 437);
            this.stxViewer.TabIndex = 2;
            this.stxViewer.TabStop = false;
            this.stxViewer.Text = "String Viewer";
            this.stxViewer.Visible = false;
            // 
            // currentSTXStringList
            // 
            this.currentSTXStringList.AllowUserToResizeColumns = false;
            this.currentSTXStringList.AllowUserToResizeRows = false;
            this.currentSTXStringList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.currentSTXStringList.BackgroundColor = System.Drawing.SystemColors.Window;
            this.currentSTXStringList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.currentSTXStringList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.currentSTXStringList.ColumnHeadersVisible = false;
            this.currentSTXStringList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Value});
            this.currentSTXStringList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.currentSTXStringList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentSTXStringList.Location = new System.Drawing.Point(3, 16);
            this.currentSTXStringList.Name = "currentSTXStringList";
            this.currentSTXStringList.RowHeadersVisible = false;
            this.currentSTXStringList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.currentSTXStringList.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.currentSTXStringList.RowTemplate.Height = 16;
            this.currentSTXStringList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.currentSTXStringList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.currentSTXStringList.Size = new System.Drawing.Size(536, 418);
            this.currentSTXStringList.TabIndex = 0;
            this.currentSTXStringList.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.currentSTXStringList_CellValueChanged);
            this.currentSTXStringList.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.currentSTXStringList_UserAddedRow);
            this.currentSTXStringList.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.currentSTXStringList_UserDeletedRow);
            // 
            // Value
            // 
            this.Value.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Value.DataPropertyName = "Value";
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            // 
            // wrdViewer
            // 
            this.wrdViewer.Controls.Add(this.tabPage1);
            this.wrdViewer.Controls.Add(this.tabPage2);
            this.wrdViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wrdViewer.Location = new System.Drawing.Point(0, 0);
            this.wrdViewer.Name = "wrdViewer";
            this.wrdViewer.SelectedIndex = 0;
            this.wrdViewer.Size = new System.Drawing.Size(542, 437);
            this.wrdViewer.TabIndex = 3;
            this.wrdViewer.Visible = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(534, 411);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Script Editor";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.MinimumSize = new System.Drawing.Size(418, 418);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.panel1);
            this.splitContainer2.Panel1.Controls.Add(this.currentWRDCommandList);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer2.Size = new System.Drawing.Size(528, 418);
            this.splitContainer2.SplitterDistance = 251;
            this.splitContainer2.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.cmdMoveDown);
            this.panel1.Controls.Add(this.cmdMoveUp);
            this.panel1.Controls.Add(this.cmdDelete);
            this.panel1.Controls.Add(this.cmdAdd);
            this.panel1.Location = new System.Drawing.Point(497, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(32, 244);
            this.panel1.TabIndex = 2;
            // 
            // cmdMoveDown
            // 
            this.cmdMoveDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdMoveDown.Location = new System.Drawing.Point(3, 214);
            this.cmdMoveDown.Name = "cmdMoveDown";
            this.cmdMoveDown.Size = new System.Drawing.Size(26, 26);
            this.cmdMoveDown.TabIndex = 4;
            this.cmdMoveDown.Text = "↓";
            this.cmdMoveDown.UseVisualStyleBackColor = true;
            // 
            // cmdMoveUp
            // 
            this.cmdMoveUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdMoveUp.Location = new System.Drawing.Point(3, 142);
            this.cmdMoveUp.Name = "cmdMoveUp";
            this.cmdMoveUp.Size = new System.Drawing.Size(26, 26);
            this.cmdMoveUp.TabIndex = 3;
            this.cmdMoveUp.Text = "↑";
            this.cmdMoveUp.UseVisualStyleBackColor = true;
            // 
            // cmdDelete
            // 
            this.cmdDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdDelete.Location = new System.Drawing.Point(3, 75);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(26, 26);
            this.cmdDelete.TabIndex = 2;
            this.cmdDelete.Text = "-";
            this.cmdDelete.UseVisualStyleBackColor = true;
            // 
            // cmdAdd
            // 
            this.cmdAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAdd.Location = new System.Drawing.Point(3, 3);
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Size = new System.Drawing.Size(26, 26);
            this.cmdAdd.TabIndex = 1;
            this.cmdAdd.Text = "+";
            this.cmdAdd.UseVisualStyleBackColor = true;
            // 
            // currentWRDCommandList
            // 
            this.currentWRDCommandList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.currentWRDCommandList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentWRDCommandList.FormattingEnabled = true;
            this.currentWRDCommandList.ItemHeight = 16;
            this.currentWRDCommandList.Location = new System.Drawing.Point(0, 0);
            this.currentWRDCommandList.Name = "currentWRDCommandList";
            this.currentWRDCommandList.Size = new System.Drawing.Size(497, 244);
            this.currentWRDCommandList.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.splitContainer3);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(528, 163);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Command Editor";
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer3.IsSplitterFixed = true;
            this.splitContainer3.Location = new System.Drawing.Point(3, 16);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.label2);
            this.splitContainer3.Panel1.Controls.Add(this.label1);
            this.splitContainer3.Panel1.Controls.Add(this.argCountBox);
            this.splitContainer3.Panel1.Controls.Add(this.opcodeComboBox);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.groupBox4);
            this.splitContainer3.Size = new System.Drawing.Size(522, 144);
            this.splitContainer3.SplitterDistance = 26;
            this.splitContainer3.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(406, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Argument Count:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Opcode:";
            // 
            // argCountBox
            // 
            this.argCountBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.argCountBox.Location = new System.Drawing.Point(526, 3);
            this.argCountBox.Name = "argCountBox";
            this.argCountBox.Size = new System.Drawing.Size(49, 20);
            this.argCountBox.TabIndex = 1;
            // 
            // opcodeComboBox
            // 
            this.opcodeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.opcodeComboBox.FormattingEnabled = true;
            this.opcodeComboBox.Location = new System.Drawing.Point(80, 2);
            this.opcodeComboBox.Name = "opcodeComboBox";
            this.opcodeComboBox.Size = new System.Drawing.Size(93, 21);
            this.opcodeComboBox.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(522, 114);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Arguments";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.currentWRDHexEditor);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(534, 411);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Hex Editor";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // currentWRDHexEditor
            // 
            this.currentWRDHexEditor.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.currentWRDHexEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.currentWRDHexEditor.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.currentWRDHexEditor.LineInfoVisible = true;
            this.currentWRDHexEditor.Location = new System.Drawing.Point(3, 3);
            this.currentWRDHexEditor.Name = "currentWRDHexEditor";
            this.currentWRDHexEditor.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.currentWRDHexEditor.Size = new System.Drawing.Size(528, 405);
            this.currentWRDHexEditor.StringViewVisible = true;
            this.currentWRDHexEditor.TabIndex = 0;
            this.currentWRDHexEditor.VScrollBarVisible = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 461);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(700, 500);
            this.Name = "MainForm";
            this.Text = "FlashbackLight [EARLY PROTOTYPE BUILD]";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.stxViewer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.currentSTXStringList)).EndInit();
            this.wrdViewer.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.argCountBox)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox currentSPCEntryList;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ListBox currentWRDCommandList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.ComboBox opcodeComboBox;
        private System.Windows.Forms.NumericUpDown argCountBox;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button cmdAdd;
        private System.Windows.Forms.Button cmdMoveDown;
        private System.Windows.Forms.Button cmdMoveUp;
        private System.Windows.Forms.Button cmdDelete;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox stxViewer;
        private System.Windows.Forms.DataGridView currentSTXStringList;
        private System.Windows.Forms.TabControl wrdViewer;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private Be.Windows.Forms.HexBox currentWRDHexEditor;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
    }
}

