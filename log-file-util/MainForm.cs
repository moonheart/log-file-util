using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimpleHelpers;

namespace log_file_util
{
    public partial class MainForm : Form
    {
        private const int DetectSize = 1024 * 1024 * 10;

        public MainForm()
        {
            InitializeComponent();
        }


        private List<string> _targetEncodings = new List<string>()
        {
            "utf-8",
            "gb18030",
        };

        private List<ConvertFile> _convertFiles;

        private void Form1_Load(object sender, EventArgs e)
        {
            cbTargetEncoding.DataSource = _targetEncodings;
        }

        private async void dataGridView1_DragDrop(object sender, DragEventArgs e)
        {
            var fileOrFolders = e.Data.GetData(DataFormats.FileDrop) as string[];
            await SetItems(fileOrFolders);
        }

        private async Task SetItems(string[] fileOrFolders)
        {
            var allFiles = _convertFiles = await GetAllFiles(fileOrFolders);

            listView1.Items.Clear();
            listView1.Items.AddRange(allFiles.Select(d =>
            {
                var lvi = new ListViewItem
                {
                    Text = d.FileName
                };
                lvi.SubItems.Add(d.Encoding?.EncodingName);
                lvi.SubItems.Add(d.SizeInBytes.ToString());
                return lvi;
            }).ToArray());

            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private async Task<List<ConvertFile>> GetAllFiles(string[] fileOrDirectories)
        {
            var files = new List<ConvertFile>();
            foreach (var fileOrDirectory in fileOrDirectories)
            {
                var fileAttributes = File.GetAttributes(fileOrDirectory);
                if ((fileAttributes & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    var subFiles = Directory.GetFiles(fileOrDirectory);
                    files.AddRange(await GetAllFiles(subFiles));
                }
                else
                {
                    var fileInfo = new FileInfo(fileOrDirectory);

                    byte[] buffer = new byte[fileInfo.Length > DetectSize ? DetectSize : fileInfo.Length];
                    using (var fileStream = new FileStream(fileOrDirectory, FileMode.Open))
                    {
                        await fileStream.ReadAsync(buffer, 0, buffer.Length);
                    }

                    var checkForTextualData = FileEncoding.CheckForTextualData(buffer);
                    if (!checkForTextualData)
                    {
                        Console.WriteLine("not text");
                    }
                    else
                    {
                        var detectFileEncoding = FileEncoding.DetectFileEncoding(buffer, 0, buffer.Length);
                        var convertFile = new ConvertFile {Encoding = detectFileEncoding, FileInfo = fileInfo};
                        files.Add(convertFile);
                    }
                }
            }

            return files;
        }


        private void dataGridView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private async void btnConvertEncoding_Click(object sender, EventArgs e)
        {
            if (_convertFiles == null) return;

            var targetEncoding = Encoding.GetEncoding(cbTargetEncoding.Text);

            await ProcessWithProgress(file =>
            {
                if (!Equals(file.Encoding, targetEncoding))
                {
                    var readAllText = File.ReadAllText(file.FileName, file.Encoding);
                    File.WriteAllText(file.FileName, readAllText, targetEncoding);
                }
            });

            await SetItems(_convertFiles.Select(d => d.FileName).ToArray());
        }

        private async void btnRegexReplace_Click(object sender, EventArgs e)
        {
            if (_convertFiles == null) return;

            if (string.IsNullOrWhiteSpace(tbRegex.Text)) return;

            var regex = new Regex(tbRegex.Text,
                RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);

            await ProcessWithProgress(file =>
            {
                var readAllText = File.ReadAllText(file.FileName, file.Encoding);
                readAllText = regex.Replace(readAllText, tbReplacement.Text);
                File.WriteAllText(file.FileName, readAllText, file.Encoding);
            });
        }

        private async Task ProcessWithProgress(Action<ConvertFile> processAction)
        {
            SetControlState(this.Controls.Cast<Control>(), false);
            try
            {
                progressBar1.Value = 0;
                progressBar1.Minimum = 0;
                progressBar1.Maximum = _convertFiles.Count;

                await Task.Run(() =>
                {
                    Parallel.ForEach(_convertFiles, new ParallelOptions() {MaxDegreeOfParallelism = Environment.ProcessorCount},
                        file =>
                        {
                            processAction(file);
                            progressBar1.Invoke((() => progressBar1.Value++));
                        });
                });
            }
            finally
            {
                SetControlState(this.Controls.Cast<Control>(), true);
            }
        }

        private void SetControlState(IEnumerable<Control> controls, bool enabled)
        {
            foreach (Control control in controls)
            {
                control.Enabled = enabled;
                SetControlState(control.Controls.Cast<Control>(), enabled);
            }
        }
    }
}