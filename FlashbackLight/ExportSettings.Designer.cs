namespace FlashbackLight
{
    partial class ExportSettings
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
            this.stxExportFiletype = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.wrdExportFiletype = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.exportFiles = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // stxExportFiletype
            // 
            this.stxExportFiletype.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.stxExportFiletype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.stxExportFiletype.FormattingEnabled = true;
            this.stxExportFiletype.Items.AddRange(new object[] {
            "STX",
            "TXT"});
            this.stxExportFiletype.Location = new System.Drawing.Point(6, 21);
            this.stxExportFiletype.Name = "stxExportFiletype";
            this.stxExportFiletype.Size = new System.Drawing.Size(125, 24);
            this.stxExportFiletype.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.stxExportFiletype);
            this.groupBox1.Location = new System.Drawing.Point(162, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(137, 51);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "STX";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.wrdExportFiletype);
            this.groupBox2.Location = new System.Drawing.Point(12, 29);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(137, 51);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "WRD";
            // 
            // wrdExportFiletype
            // 
            this.wrdExportFiletype.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wrdExportFiletype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.wrdExportFiletype.FormattingEnabled = true;
            this.wrdExportFiletype.Items.AddRange(new object[] {
            "WRD"});
            this.wrdExportFiletype.Location = new System.Drawing.Point(6, 21);
            this.wrdExportFiletype.Name = "wrdExportFiletype";
            this.wrdExportFiletype.Size = new System.Drawing.Size(125, 24);
            this.wrdExportFiletype.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Select Export Filetypes";
            // 
            // exportFiles
            // 
            this.exportFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.exportFiles.Location = new System.Drawing.Point(12, 87);
            this.exportFiles.Name = "exportFiles";
            this.exportFiles.Size = new System.Drawing.Size(287, 32);
            this.exportFiles.TabIndex = 1;
            this.exportFiles.Text = "Export Files";
            this.exportFiles.UseVisualStyleBackColor = true;
            this.exportFiles.Click += new System.EventHandler(this.exportFiles_Click);
            // 
            // ExportSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(311, 131);
            this.Controls.Add(this.exportFiles);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ExportSettings";
            this.Text = "ExportSettings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox stxExportFiletype;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox wrdExportFiletype;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button exportFiles;
    }
}