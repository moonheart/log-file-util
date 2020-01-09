using System.IO;
using System.Text;

namespace log_file_util
{
    public class ConvertFile
    {
        public string FileName { get; set; }
        public Encoding Encoding { get; set; }

        public long SizeInBytes { get; set; }

    }
}