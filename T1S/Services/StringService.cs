using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using T1S.Utils.Constants;

namespace T1S.Services
{
    public static class StringService
    {
        public static ObservableCollection<string> Extract(string path)
        {
            ObservableCollection<string> result = new ObservableCollection<string>();
            const int bufferSize = 4096;
            byte[] buffer = new byte[bufferSize];
            int bytesRead;
            long fileOffset = 0;
            using FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            using BinaryReader reader = new BinaryReader(stream);
            StringBuilder utf8Builder = new StringBuilder();

            while ((bytesRead = reader.Read(buffer, 0, bufferSize)) > 0)
            {
                for (int i = 0; i < bytesRead; i++)
                {
                    if (buffer[i] >= 0x20 && buffer[i] <= 0x7E)
                    {
                        utf8Builder.Append((char)buffer[i]);
                    }
                    else if (utf8Builder.Length >= 8)
                    {
                        result.Add(utf8Builder.ToString());
                        utf8Builder.Clear();
                    }

                    fileOffset += bytesRead;
                }
            }

            return result;
        }
    }
}
