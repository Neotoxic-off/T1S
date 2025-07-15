using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace T1S.Utils
{
    public static class Random
    {
        public static byte[] Bytes()
        {
            byte[]? result = null;

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(result);
            }

            return result;
        }

        public static string? StringInArray(string[] items)
        {
            string? result = null;
            System.Random rand = new System.Random();

            if (items.Length > 0)
            {
                result = items[rand.Next(0, items.Length)];
            }

            return result;
        }
    }
}
