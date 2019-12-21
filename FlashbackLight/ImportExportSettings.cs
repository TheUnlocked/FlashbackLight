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
    public partial class ImportExportSettings : Form
    {
        public enum Mode
        {
            Import,
            Export
        }

        private Mode mode;

        public ImportExportSettings(Mode mode)
        {
            InitializeComponent();
            this.mode = mode;
            wrdFiletype.SelectedIndex = 0;
            stxFiletype.SelectedIndex = 0;
            filetypeGroupLabel.Text = @$"Select {mode switch
            {
                Mode.Import => "Import",
                Mode.Export => "Export",
                _ => "You shouldn't be seeing this..."
            }} Filetypes";
            handleFiles.Text = mode switch
            {
                Mode.Import => "Import Files",
                Mode.Export => "Export Files",
                _ => "You shouldn't be seeing this..."
            };
        }

        private void handleFiles_Click(object sender, EventArgs e)
        {
            if (mode switch
            {
                Mode.Import => ImportFiles(),
                Mode.Export => ExportFiles(),
                _ => false
            })
            {
                Close();
            }
        }

        private bool ImportFiles()
        {
            MessageBox.Show("This is not currently implemented :(");
            return false;
        }

        private bool ExportFiles()
        {
            using CommonOpenFileDialog dialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true
            };
            var result = dialog.ShowDialog();
            if (result != CommonFileDialogResult.Ok)
                return false;
            foreach (var entry in MainForm.currentSPC.Entries)
            {
                var loadedFile = entry.Value.Load();
                string path = Path.Combine(dialog.FileName, entry.Key);
                string targetExt = loadedFile switch
                {
                    WRD _ => wrdFiletype.Text,
                    STX _ => stxFiletype.Text,
                    _ => ""
                };
                path = Path.ChangeExtension(path, targetExt.ToLower());
                File.WriteAllBytes(path, loadedFile.FileConversions[targetExt].toBytes());
            }
            return true;
        }
    }
}
