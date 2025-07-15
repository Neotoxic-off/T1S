using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using T1S.Utils.Constants;

namespace T1S.Services
{
    public static class AntiDebugService
    {
        public static List<string> Scan(string path)
        {
            var results = new List<string>();
            using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var reader = new BinaryReader(stream);

            const int bufferSize = 4096;
            byte[] buffer = new byte[bufferSize];
            int bytesRead;
            long fileOffset = 0;
            StringBuilder utf8Builder = new StringBuilder();

            while ((bytesRead = reader.Read(buffer, 0, bufferSize)) > 0)
            {
                for (int i = 0; i < bytesRead; i++)
                {
                    byte b = buffer[i];
                    if (b >= 0x20 && b <= 0x7E)
                    {
                        utf8Builder.Append((char)b);
                    }
                    else
                    {
                        FlushUtf8(results, utf8Builder);
                    }
                }

                fileOffset += bytesRead;
            }

            FlushUtf8(results, utf8Builder);

            return results;
        }

        private static void FlushUtf8(List<string> results, StringBuilder builder)
        {
            if (builder.Length >= 4)
            {
                string str = builder.ToString();
                foreach (var api in WindowsAPI.AntiDebugAPI)
                {
                    if (str.Contains(api, StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show($"Found anti-debug string: {api}");
                        break;
                    }
                }
            }

            builder.Clear();
        }
    }
}
