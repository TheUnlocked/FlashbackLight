using FlashbackLight.Formats;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlashbackLight
{
    public partial class ExportSettings : Form
    {
        public ExportSettings()
        {
            InitializeComponent();
            wrdExportFiletype.SelectedIndex = 0;
            stxExportFiletype.SelectedIndex = 0;
        }

        private void exportFiles_Click(object sender, EventArgs e)
        {
            using CommonOpenFileDialog dialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true
            };
            var result = dialog.ShowDialog();
            if (result != CommonFileDialogResult.Ok)
                return;
            foreach (var entry in MainForm.currentSPC.Entries)
            {
                var loadedFile = entry.Value.Load();
                string path = Path.Combine(dialog.FileName, entry.Key);
                string targetExt = loadedFile switch
                {
                    WRD _ => wrdExportFiletype.Text,
                    STX _ => stxExportFiletype.Text,
                    _ => ""
                };
                path = Path.ChangeExtension(path, targetExt.ToLower());
                File.WriteAllBytes(path, loadedFile.FileConversions[targetExt].toBytes());
            }
            Close();
        }
    }
}
