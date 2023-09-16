using System.Security.Cryptography;
using System.Text;

namespace Webfuel
{
    public interface ITimeLimitedTokenService
    {
        string EncodeToken(string content, string key, DateTime expiryUtc);

        string DecodeToken(string token, string key);
    }

    [ServiceImplementation(typeof(ITimeLimitedTokenService))]
    class TimeLimitedTokenService : ITimeLimitedTokenService
    {
        const string EncryptKey = "Jajkcvc9933ASkx32jsas";
        const string EncryptSalt = "RakuuvdslDASFasdkjf";

        public string EncodeToken(string content, string key, DateTime expiryUtc)
        {
            if (key.StartsWith("::"))
                key = "127.0.0.1";

            var hash = GenerateHash(content, expiryUtc, key);

            var plain =
                content + ":"
                + expiryUtc.Ticks + ":"
                + key + ":"
                + hash;

            var token = Encrypt(plain, EncryptKey);

            return token;
        }

        public string DecodeToken(string token, string key)
        {
            if (String.IsNullOrEmpty(token))
                return String.Empty;

            try
            {
                var plain = Decrypt(token, EncryptKey);

                var parts = plain.Split(new char[] { ':' });
                if (parts.Length != 4)
                    return String.Empty;

                var content = parts[0];
                var expiryUtc = new DateTime(long.Parse(parts[1]), DateTimeKind.Utc);
                var _key = parts[2];
                var hash = parts[3];

                if (expiryUtc < DateTime.UtcNow)
                    return String.Empty; // Token has expired

                if (_key != key)
                    return String.Empty; // Token does not have a matching key

                if (hash != GenerateHash(content, expiryUtc, key))
                    return String.Empty; // Hash is invalid

                return content;
            }
            catch
            {
                return String.Empty;
            }
        }

        string GenerateHash(string content, DateTime expiryUtc, string key)
        {
            return SHA256Hash(content + ":" + expiryUtc.Ticks + ":" + key + ":" + EncryptSalt);
        }

        /// <summary>
        /// Calculates the SHA256 hash of the given string
        /// </summary>
        string SHA256Hash(string input)
        {
            if (String.IsNullOrEmpty(input))
                return input;
            var inputBytes = Encoding.UTF8.GetBytes(input);
            var hashAlgorithm = SHA256.Create();
            var hashedBytes = hashAlgorithm.ComputeHash(inputBytes);
            return Convert.ToBase64String(hashedBytes);
        }

        /// <summary>
        /// Encrypts the plainText input using the given Key.
        /// A 128 bit random salt will be generated and prepended to the ciphertext before it is base64 encoded.
        /// </summary>
        /// <param name="plainText">The plain text to encrypt.</param>
        /// <param name="key">The plain text encryption key.</param>
        /// <returns>The salt and the ciphertext, Base64 encoded for convenience.</returns>
        string Encrypt(string plainText, string key)
        {
            var saltSize = 32;

            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentNullException("plainText");
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");

            // Derive a new Salt and IV from the Key
            using (var keyDerivationFunction = new Rfc2898DeriveBytes(key, saltSize, 1, HashAlgorithmName.SHA256))
            {
                var saltBytes = keyDerivationFunction.Salt;
                var keyBytes = keyDerivationFunction.GetBytes(32);
                var ivBytes = keyDerivationFunction.GetBytes(16);

                // Create an encryptor to perform the stream transform.
                // Create the streams used for encryption.
                using (var aesManaged = Aes.Create())
                using (var encryptor = aesManaged.CreateEncryptor(keyBytes, ivBytes))
                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    using (var streamWriter = new StreamWriter(cryptoStream))
                    {
                        // Send the data through the StreamWriter, through the CryptoStream, to the underlying MemoryStream
                        streamWriter.Write(plainText);
                    }

                    // Return the encrypted bytes from the memory stream, in Base64 form so we can send it right to a database (if we want).
                    var cipherTextBytes = memoryStream.ToArray();
                    Array.Resize(ref saltBytes, saltBytes.Length + cipherTextBytes.Length);
                    Array.Copy(cipherTextBytes, 0, saltBytes, saltSize, cipherTextBytes.Length);

                    return Convert.ToBase64String(saltBytes);
                }
            }
        }

        /// <summary>
        /// Decrypts the ciphertext using the Key.
        /// </summary>
        /// <param name="cipherText">The ciphertext to decrypt.</param>
        /// <param name="key">The plain text encryption key.</param>
        /// <returns>The decrypted text.</returns>
        string Decrypt(string cipherText, string key)
        {
            var saltSize = 32;

            if (string.IsNullOrEmpty(cipherText))
                throw new ArgumentNullException("cipherText");
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");

            // Extract the salt from our ciphertext
            var allTheBytes = Convert.FromBase64String(cipherText);
            var saltBytes = allTheBytes.Take(saltSize).ToArray();
            var ciphertextBytes = allTheBytes.Skip(saltSize).Take(allTheBytes.Length - saltSize).ToArray();

            using (var keyDerivationFunction = new Rfc2898DeriveBytes(key, saltSize, 1, HashAlgorithmName.SHA256))
            {
                // Derive the previous IV from the Key and Salt
                var keyBytes = keyDerivationFunction.GetBytes(32);
                var ivBytes = keyDerivationFunction.GetBytes(16);

                // Create a decrytor to perform the stream transform.
                // Create the streams used for decryption.
                // The default Cipher Mode is CBC and the Padding is PKCS7 which are both good
                using (var aesManaged = Aes.Create())
                using (var decryptor = aesManaged.CreateDecryptor(keyBytes, ivBytes))
                using (var memoryStream = new MemoryStream(ciphertextBytes))
                using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                using (var streamReader = new StreamReader(cryptoStream))
                {
                    // Return the decrypted bytes from the decrypting stream.
                    return streamReader.ReadToEnd();
                }
            }
        }
    }
}
