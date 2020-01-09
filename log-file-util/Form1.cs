using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimpleHelpers;

namespace log_file_util
{
    public partial class Form1 : Form
    {
        public Form1()
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

        private void dataGridView1_DragDrop(object sender, DragEventArgs e)
        {
            var fileOrFolders = e.Data.GetData(DataFormats.FileDrop) as string[];
            SetItems(fileOrFolders);
        }

        private void SetItems(string[] fileOrFolders)
        {
            var allFiles = _convertFiles = GetAllFiles(fileOrFolders);

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
        }

        private List<ConvertFile> GetAllFiles(string[] fileOrDirectories)
        {
            var files = new List<ConvertFile>();
            foreach (var fileOrDirectory in fileOrDirectories)
            {
                var fileAttributes = File.GetAttributes(fileOrDirectory);
                if ((fileAttributes & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    var subFiles = Directory.GetFiles(fileOrDirectory);
                    files.AddRange(GetAllFiles(subFiles));
                }
                else
                {
                    var fileInfo = new FileInfo(fileOrDirectory);

                    byte[] buffer = new byte[fileInfo.Length > 1024 * 1024 * 10 ? 1024 * 1024 * 10 : fileInfo.Length];
                    using (var fileStream = new FileStream(fileOrDirectory, FileMode.Open))
                    {
                        fileStream.Read(buffer, 0, buffer.Length);
                    }

                    var checkForTextualData = FileEncoding.CheckForTextualData(buffer);
                    if (!checkForTextualData)
                    {
                        Console.WriteLine("not text");
                    }
                    else
                    {
                        var detectFileEncoding = FileEncoding.DetectFileEncoding(buffer, 0, buffer.Length);
                        var readAllText = File.ReadAllText(fileOrDirectory, detectFileEncoding);
                        var convertFile = new ConvertFile { FileName = fileOrDirectory, Encoding = detectFileEncoding, SizeInBytes = fileInfo.Length };
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

        private void btnConvertEncoding_Click(object sender, EventArgs e)
        {
            if (_convertFiles == null) return;

            var targetEncoding = Encoding.GetEncoding(cbTargetEncoding.Text);

            foreach (var convertFile in _convertFiles)
            {
                if (Equals(convertFile.Encoding, targetEncoding))
                {
                    continue;
                }

                var readAllText = File.ReadAllText(convertFile.FileName, convertFile.Encoding);
                File.WriteAllText(convertFile.FileName, readAllText, targetEncoding);
            }

            SetItems(_convertFiles.Select(d => d.FileName).ToArray());
        }

        private async void btnRegexReplace_Click(object sender, EventArgs e)
        {
            if (_convertFiles == null) return;

            if (string.IsNullOrWhiteSpace(tbRegex.Text)) return;

            btnRegexReplace.Enabled = false;
            try
            {
                var regex = new Regex(tbRegex.Text,
                    RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);

                progressBar1.Value = 0;
                progressBar1.Minimum = 0;
                progressBar1.Maximum = _convertFiles.Count;

                await Task.Run(() =>
                {
                    Parallel.ForEach(_convertFiles, new ParallelOptions(){MaxDegreeOfParallelism = Environment.ProcessorCount},
                        file =>
                    {
                        var readAllText = File.ReadAllText(file.FileName, file.Encoding);
                        readAllText = regex.Replace(readAllText, tbReplacement.Text);
                        File.WriteAllText(file.FileName, readAllText, file.Encoding);
                        progressBar1.Invoke((() => progressBar1.Value++));
                    });
                });
            }
            finally
            {
                btnRegexReplace.Enabled = true;
            }

        }
    }
}