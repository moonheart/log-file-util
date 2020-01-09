using System.IO;
using System.Text;

namespace log_file_util
{
    public class ConvertFile
    {
        public string FileName => FileInfo.FullName;
        public Encoding Encoding { get; set; }

        public long SizeInBytes => FileInfo.Length;
        public FileInfo FileInfo { get; set; }

    }
}