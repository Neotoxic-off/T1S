using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T1S.Models.PE;
using T1S.Models.Scan;

namespace T1S.Utils
{
    public static class Entropy
    {
        public static double CalculateEntropy(byte[] data)
        {
            int[] counts = new int[256];
            double entropy = 0.0;

            if (data.Length != 0)
            {
                foreach (byte b in data)
                {
                    counts[b]++;
                }
                foreach (int count in counts)
                {
                    if (count == 0) continue;
                    double p = (double)count / data.Length;
                    entropy -= p * Math.Log(p, 2);
                }

                return entropy;
            }

            return 0.0;
        }

        public static Sections GetSectionEntropies(string path, List<PESection> sections)
        {
            Sections result = new Sections();
            Dictionary<string, Section> binding = new Dictionary<string, Section>()
            {
                { ".text", result.text },
                { ".bss", result.bss },
                { ".rdata", result.rdata },
                { ".edata", result.edata },
                { ".idata", result.idata },
                { ".reloc", result.reloc },
                { ".rsrc", result.rsrc },
                { ".tls", result.tls }
            };
            byte[] data = new byte[0];
            using var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            using var reader = new BinaryReader(fs);

            foreach (PESection section in sections)
            {
                if (section.PointerToRawData == 0 || section.SizeOfRawData == 0)
                    continue;

                fs.Seek(section.PointerToRawData, SeekOrigin.Begin);
                data = reader.ReadBytes((int)section.SizeOfRawData);
                if (binding.ContainsKey(section.Name))
                {
                    binding[section.Name].Entropy = CalculateEntropy(data);
                }
            }

            return result;
        }
    }
}
