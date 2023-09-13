using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Webfuel
{
    public static class PasswordUtility
    {
        public static string CreateSalt()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random((int)DateTime.Now.Ticks);
            var result = new string(
                Enumerable.Repeat(chars, 16)
                    .Select(s => s[random.Next(s.Length)])
                    .ToArray());
            return result;
        }

        // DO NOT CHANGE THIS STRING - IF YOU DO ALL PASSWORDS WILL NO LONGER WORK
        const string Pepper = "asdSSD934h!£jksda93ASd£$$9zsdcgdfsdsrzsddf";

        public static string CreateHash(string password, string salt)
        {
            SHA256 hash = SHA256.Create();
            var encoder = new ASCIIEncoding();
            byte[] input = encoder.GetBytes(salt + Pepper + password + salt);
            // Double hash
            input = hash.ComputeHash(input);
            input = hash.ComputeHash(input);
            return Convert.ToBase64String(input);
        }
    }
}
