namespace FlashbackLight
{
    partial class ImportExportSettings
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
            this.stxFiletype = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.wrdFiletype = new System.Windows.Forms.ComboBox();
            this.filetypeGroupLabel = new System.Windows.Forms.Label();
            this.handleFiles = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // stxFiletype
            // 
            this.stxFiletype.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.stxFiletype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.stxFiletype.FormattingEnabled = true;
            this.stxFiletype.Items.AddRange(new object[] {
            "STX",
            "TXT"});
            this.stxFiletype.Location = new System.Drawing.Point(6, 21);
            this.stxFiletype.Name = "stxFiletype";
            this.stxFiletype.Size = new System.Drawing.Size(125, 24);
            this.stxFiletype.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.stxFiletype);
            this.groupBox1.Location = new System.Drawing.Point(162, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(137, 51);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "STX";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.wrdFiletype);
            this.groupBox2.Location = new System.Drawing.Point(12, 29);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(137, 51);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "WRD";
            // 
            // wrdFiletype
            // 
            this.wrdFiletype.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wrdFiletype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.wrdFiletype.FormattingEnabled = true;
            this.wrdFiletype.Items.AddRange(new object[] {
            "WRD"});
            this.wrdFiletype.Location = new System.Drawing.Point(6, 21);
            this.wrdFiletype.Name = "wrdFiletype";
            this.wrdFiletype.Size = new System.Drawing.Size(125, 24);
            this.wrdFiletype.TabIndex = 0;
            // 
            // filetypeGroupLabel
            // 
            this.filetypeGroupLabel.AutoSize = true;
            this.filetypeGroupLabel.Location = new System.Drawing.Point(12, 9);
            this.filetypeGroupLabel.Name = "filetypeGroupLabel";
            this.filetypeGroupLabel.Size = new System.Drawing.Size(151, 17);
            this.filetypeGroupLabel.TabIndex = 3;
            this.filetypeGroupLabel.Text = "Select Export Filetypes";
            // 
            // handleFiles
            // 
            this.handleFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.handleFiles.Location = new System.Drawing.Point(12, 87);
            this.handleFiles.Name = "handleFiles";
            this.handleFiles.Size = new System.Drawing.Size(287, 32);
            this.handleFiles.TabIndex = 1;
            this.handleFiles.Text = "Export Files";
            this.handleFiles.UseVisualStyleBackColor = true;
            this.handleFiles.Click += new System.EventHandler(this.handleFiles_Click);
            // 
            // ImportExportSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(311, 131);
            this.Controls.Add(this.handleFiles);
            this.Controls.Add(this.filetypeGroupLabel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ImportExportSettings";
            this.Text = "Import/Export";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox stxFiletype;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox wrdFiletype;
        private System.Windows.Forms.Label filetypeGroupLabel;
        private System.Windows.Forms.Button handleFiles;
    }
}