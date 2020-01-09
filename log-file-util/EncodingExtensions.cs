using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace log_file_util
{
    /// <summary>
    /// 编码扩展
    /// </summary>
    public static class EncodingExtensions
    {
        private const int Utf8PreambleLength = 3;
        private const byte Utf8PreambleByte2 = 0xBF;
        private const int Utf8PreambleFirst2Bytes = 0xEFBB;

        // UTF32 not supported on Phone
        private const int Utf32PreambleLength = 4;
        private const byte Utf32PreambleByte2 = 0x00;
        private const byte Utf32PreambleByte3 = 0x00;
        private const int Utf32OrUnicodePreambleFirst2Bytes = 0xFFFE;
        private const int BigEndianUnicodePreambleFirst2Bytes = 0xFEFF;

        /// <summary>
        /// 检测编码类型
        /// </summary>
        /// <param name="filePath">被检测的编码</param>
        /// <returns>编码类型</returns>
        public static Encoding GetEncoding(string filePath)
        {
            byte[] buffer;
            using (var fs = new FileStream(filePath, FileMode.Open))
            {
                buffer = new byte[255];
                fs.Read(buffer, 0, 255);
            }

            var bytes = new ArraySegment<byte>(buffer);
            return DetectEncoding(bytes);
        }

        private static Encoding DetectEncoding(ArraySegment<byte> buffer)
        {
            byte[] data = buffer.Array;
            int offset = buffer.Offset;
            int dataLength = buffer.Count;


            if (dataLength >= 2 && data != null)
            {
                int first2Bytes = data[offset + 0] << 8 | data[offset + 1];

                switch (first2Bytes)
                {
                    case Utf8PreambleFirst2Bytes:
                        if (dataLength >= Utf8PreambleLength && data[offset + 2] == Utf8PreambleByte2)
                        {
                            return Encoding.UTF8;
                        }

                        break;

                    case Utf32OrUnicodePreambleFirst2Bytes:
#if !NETNative
                        // UTF32 not supported on Phone
                        if (dataLength >= Utf32PreambleLength && data[offset + 2] == Utf32PreambleByte2 && data[offset + 3] == Utf32PreambleByte3)
                        {
                            return Encoding.UTF32;
                        }
                        else
#endif
                        {
                            return Encoding.Unicode;
                        }

                    case BigEndianUnicodePreambleFirst2Bytes:
                        return Encoding.BigEndianUnicode;
                }
            }

            return Encoding.UTF8;
        }
    }
}