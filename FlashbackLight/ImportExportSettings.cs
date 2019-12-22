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
            using CommonOpenFileDialog dialog = new CommonOpenFileDialog
            {
                Multiselect = true,
            };
            var result = dialog.ShowDialog();
            if (result != CommonFileDialogResult.Ok)
                return false;

            foreach (var filename in dialog.FileNames)
            {
                var fileExt = Path.GetExtension(filename).Substring(1).ToUpper();
                string sourceExt;
                if (fileExt == wrdFiletype.Text)
                    sourceExt = "wrd";
                else if (fileExt == stxFiletype.Text)
                    sourceExt = "stx";
                else if (new[] { "stx", "wrd" }.Contains(fileExt))
                    sourceExt = fileExt.ToLower();
                else
                    continue;

                var sourceFilename = Path.GetFileName(Path.ChangeExtension(filename, sourceExt));

                if (MainForm.currentSPC.Entries.TryGetValue(sourceFilename, out var entry)) {
                    V3Format data = sourceExt switch
                    {
                        "wrd" => new WRD(entry, MainForm.currentSPCFilename, sourceFilename),
                        "stx" => new STX(entry),
                        _ => null
                    };
                    if (data != null)
                    {
                        data.FileConversions[fileExt.ToUpper()].fromBytes(File.ReadAllBytes(filename));
                        data.UpdateSPCEntry();
                        data.Recompress();
                    }
                }
                else
                {
                    MessageBox.Show($"Importing new files to an SPC is not currently supported.");
                    continue;
                }
            }

            return true;
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
