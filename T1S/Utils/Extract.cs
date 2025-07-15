using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T1S.Utils
{
    public static class Extract
    {
        private static List<string> Strings(byte[] bytes, int minLength)
        {
            List<string> result = new List<string>();
            StringBuilder sb = new StringBuilder();

            foreach (var b in bytes)
            {
                if (b >= 0x20 && b <= 0x7E)
                {
                    sb.Append((char)b);
                }
                else
                {
                    if (sb.Length >= minLength)
                    {
                        result.Add(sb.ToString());
                    }
                    sb.Clear();
                }
            }

            if (sb.Length >= minLength)
                result.Add(sb.ToString());

            return result;
        }
    }
}
