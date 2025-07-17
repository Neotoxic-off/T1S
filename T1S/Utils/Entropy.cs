using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T1S.Models.PE;

namespace T1S.Utils
{
    public static class Entropy
    {
        public static double CalculateEntropy(byte[] data)
        {
            if (data.Length == 0) return 0.0;

            int[] counts = new int[256];
            foreach (byte b in data)
                counts[b]++;

            double entropy = 0.0;
            foreach (int count in counts)
            {
                if (count == 0) continue;
                double p = (double)count / data.Length;
                entropy -= p * Math.Log(p, 2);
            }

            return entropy;
        }

        public static Dictionary<string, double> GetSectionEntropies(string path, List<PESection> sections)
        {
            using var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            using var reader = new BinaryReader(fs);
            Dictionary<string, double> result = new Dictionary<string, double>();

            foreach (PESection section in sections)
            {
                if (section.PointerToRawData == 0 || section.SizeOfRawData == 0)
                    continue;

                fs.Seek(section.PointerToRawData, SeekOrigin.Begin);
                byte[] data = reader.ReadBytes((int)section.SizeOfRawData);

                double entropy = CalculateEntropy(data);
                result[section.Name] = entropy;
            }

            return result;
        }
    }

}
