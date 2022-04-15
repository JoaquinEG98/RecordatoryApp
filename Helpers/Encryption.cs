using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public class Encryption
    {
        public string Hash(string text)
        {
            var textToHash = Encoding.UTF8.GetBytes(text);
            using (SHA512 alg = SHA512.Create())
            {
                string hash = "";

                var hashValue = alg.ComputeHash(textToHash);
                foreach (byte x in hashValue)
                {
                    hash += String.Format("{0:x2}", x);
                }
                return hash;
            }
        }
    }
}
