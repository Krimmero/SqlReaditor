using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SqlReaditor.Helpers
{
    class Salting
    {
        public string GetSalt()
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var salts = BitConverter.ToString(salt).Replace("-", "").ToLower();
            return salts;
        }
    }
}
