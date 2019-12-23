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
using FlashbackLight.Formats;

namespace FlashbackLight
{
    public partial class MainForm : Form
    {
        public static string RegionString = "US";
        public static string currentSPCFilename;
        public static SPC currentSPC;
        public static (string filename, V3Format data) currentFile = ("", null);

        public MainForm()
        {
            InitializeComponent();
        }

        public void OpenDataEntry(string entryName)
        {
            if (currentFile.data != null)
            {
                SaveOpenFile();
            }

            if (currentSPC.Entries.TryGetValue(entryName, out var entry))
            {
                V3Format loaded = entry.Load();
                currentFile = (entryName, loaded);
            }
            else
            {
                currentFile = ("", null);
            }

            DisplayCurrentFile();
        }

        private void DisplayCurrentFile()
        {
            wrdViewer.Visible = false;
            stxViewer.Visible = false;

            if (currentFile.data == null)
            {
                exportToolStripMenuItem.Enabled = false;
                return;
            }

            switch (currentFile.data)
            {
                case WRD stx:
                    wrdViewer.Visible = true;
                    RefreshWRDCommandList(stx);
                    break;
                case STX stx:
                    stxViewer.Visible = true;
                    RefreshSTXStringList(stx);
                    break;
                default:
                    break;
            }
            exportToolStripMenuItem.Enabled = true;
        }

        private void RefreshWRDCommandList(WRD wrd)
        {
            currentWRDHexEditor.ByteProvider = new Be.Windows.Forms.DynamicByteProvider(wrd.entry.Contents);
            currentWRDCommandList.Items.Clear();
            foreach (WRDCmd cmd in wrd.Code)
            {
                string commandString = cmd.Name + "(";
                for (int i = 0; i < cmd.ArgData.Length; i++)
                {
                    ushort arg = cmd.ArgData[i];
                    string argString = "!ERROR!";

                    // Use modulus to prevent out-of-range errors for variable-length opcodes
                    // and for invalid parameter lengths. We'll handle those afterwards.
                    byte argtype = cmd.ArgTypes[i % cmd.ArgTypes.Length];
                    switch (argtype)
                    {
                        case 0: // Plaintext Parameter
                            argString = arg < wrd.Params.Count ? wrd.Params[arg] : '!' + arg.ToString() + '!';
                            break;

                        case 1: // Raw number
                            argString = arg.ToString();
                            break;

                        case 2: // Dialog string
                            argString = arg < wrd.Strings.Count ? '"' + wrd.Strings[arg] + '"' : '!' + arg.ToString() + '!';
                            break;

                        case 3: // Label name
                            argString = arg < wrd.Labels.Count ? wrd.Labels[arg] : '!' + arg.ToString() + '!';
                            break;
                    }

                    // Check if we have too many arguments
                    if (!cmd.IsVarLength && i >= cmd.ArgTypes.Length)
                    {
                        argString.Insert(0, "!");
                        argString.Insert(argString.Length, "!");    // Should this be (argString.Length - 1)?
                    }

                    if (i + 1 < cmd.ArgData.Length && !cmd.IsVarLength)
                    {
                        argString += ", ";
                    }

                    commandString += argString;
                }
                commandString += ")";

                currentWRDCommandList.Items.Add(commandString);
            }
        }

        private void RefreshSTXStringList(STX stx)
        {
            currentSTXStringList.DataSource = new BindingSource
            {
                DataSource = new Utils.DataGridViewList<string>(stx.Strings)
            };
        }

        private void ShowOpenErrorBox(Exception error, string filepath)
        {
            if (MessageBox.Show($"Failed to open {filepath}: \n\n{error.Message}\n\n{error.StackTrace}\n\nWould you like to copy this error to your clipboard?",
                                    $"{Path.GetExtension(filepath)} Open Error: {error.Message}",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Error,
                                    MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                Clipboard.SetText($"{error.Message}\n\n{error.StackTrace}");
            }
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using OpenFileDialog fd = new OpenFileDialog
            {
                Filter = "SPC File (*.spc)|*.spc|All Files (*.*)|*.*"
            };
            fd.ShowDialog();
            string filepath = fd.FileName;
            if (filepath == "")
            {
                return;
            }
            else if (Path.GetExtension(filepath).ToLower() != ".spc")
            {
                if (MessageBox.Show("Selected file does not have the .SPC file extension and may not load properly. Attempt to open anyways?",
                                    "SPC Extension Warning",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                {
                    return;
                }
            }
            if (File.Exists(filepath))
            {
                try
                {
                    byte[] filedata;
                    filedata = File.ReadAllBytes(filepath);
                    currentSPC = new SPC();
                    currentSPC.FromBytesDefault(filedata);
                    currentSPCFilename = filepath;
                }
                catch (Exception error)
                {
                    ShowOpenErrorBox(error, filepath);
                    return;
                }
            }

            currentSPCEntryList.Items.Clear();
            currentFile = ("", null);
            DisplayCurrentFile();
            foreach (string entryName in currentSPC.Entries.Keys)
            {
                currentSPCEntryList.Items.Add(entryName);
            }
            saveToolStripMenuItem.Enabled = true;
            saveAsToolStripMenuItem.Enabled = true;
            importExportToolStripMenuItem.Enabled = true;
        }

        private void CurrentSPCEntryList_DoubleClick(object sender, EventArgs e)
        {
            if (currentSPC == null)
                return;

            string entryFilename = currentSPC.Entries.Keys.ToArray()[currentSPCEntryList.SelectedIndex];
            try
            {
                OpenDataEntry(entryFilename);
            }
            catch (Exception error)
            {
                ShowOpenErrorBox(error, entryFilename);
                return;
            }
        }

        private void Save(object sender, EventArgs e)
        {
            if (!File.Exists(currentSPCFilename))
            {
                SaveAs(sender, e);
                return;
            }
            SaveOpenFile();
            File.WriteAllBytes(currentSPCFilename, currentSPC.ToBytesDefault());
        }

        private void SaveAs(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog
            {
                AddExtension = true,
                DefaultExt = ".spc",
                Filter = $"{filetypeNames["SPC"]} File (*.spc)|*.spc|All Files (*.*)|*.*"
            };
            dialog.ShowDialog();
            if (dialog.FileName == "")
                return;
            SaveOpenFile();
            Directory.CreateDirectory(Path.GetPathRoot(dialog.FileName));
            File.WriteAllBytes(dialog.FileName, currentSPC.ToBytesDefault());
            currentSPCFilename = dialog.FileName;
        }

        private void currentSTXStringList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (currentFile.data is STX)
            {
                SaveSTX();
            }
        }

        private void currentSTXStringList_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
            => SaveSTX();

        private void currentSTXStringList_UserAddedRow(object sender, DataGridViewRowEventArgs e)
            => SaveSTX();

        private void SaveSTX()
        {
            Utils.DataGridViewList<string> list = (Utils.DataGridViewList<string>)((BindingSource)currentSTXStringList.DataSource).DataSource;
            list.PushData();
        }

        private void SaveOpenFile()
        {
            if (currentFile.data != null)
            {
                currentFile.data.UpdateSPCEntry();
                currentFile.data.Recompress();
                currentFile = ("", null);
            }
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var exportForm = new ImportExportSettings(this, ImportExportSettings.Mode.Import);
            exportForm.ShowDialog();
        }

        static readonly Dictionary<string, string> filetypeNames = new Dictionary<string, string>
        {
            { "SPC", "SPC" },
            { "STX", "STX" },
            { "WRD", "WRD" },
            { "TXT", "Text" },
        };

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string extOptions = string.Join("|", currentFile.data.FileConversions.Keys.Select(x => $"{filetypeNames[x]} File (*.{x.ToLower()})|*.{x.ToLower()}"));
            using SaveFileDialog dialog = new SaveFileDialog
            {
                AddExtension = true,
                DefaultExt = $".{currentFile.data.FileExtensionDefault.ToLower()}",
                Filter = $"{extOptions}|All Files (*.*)|*.*"
            };
            dialog.ShowDialog();
            if (dialog.FileName == "")
                return;
            Directory.CreateDirectory(Path.GetPathRoot(dialog.FileName));

            var data = currentFile.data.FileConversions.Keys.Contains(Path.GetExtension(dialog.FileName).ToUpper()) ?
                currentFile.data.FileConversions[Path.GetExtension(dialog.FileName).ToUpper()].toBytes() :
                currentFile.data.ToBytesDefault();
            File.WriteAllBytes(dialog.FileName, data);
        }

        private void exportAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var exportForm = new ImportExportSettings(this, ImportExportSettings.Mode.Export);
            exportForm.ShowDialog();
        }
    }
}
