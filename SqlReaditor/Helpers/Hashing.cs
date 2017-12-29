using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SqlReaditor.Helpers
{
    public class Hashing
    {
        public string GetHash(string salt, string password)
        {
            SHA256 sha = SHA256.Create();
            var hashedString = sha.ComputeHash(Encoding.UTF8.GetBytes(salt + password));
            var hash = BitConverter.ToString(hashedString).Replace("-", "").ToLower();
            return hash;
        }

        
    }
}
