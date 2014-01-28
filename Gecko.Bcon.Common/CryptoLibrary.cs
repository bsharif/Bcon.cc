using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Gecko.Bcon.Common
{
    public static class CryptoLibrary
    {
        public static string GetHash(string stringToHash)
        {
            using (SHA256 shaM = new SHA256Managed())
            {
                byte[] hash = shaM.ComputeHash(GetBytes(stringToHash));
                string hexHash = BitConverter.ToString(hash);
                return hexHash.Replace("-", "");
            }
        }

        public static byte[] GetBytes(string str)
        {
            var bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
    }
}
