using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Webfuel;

public interface IIdentityTokenService
{
    Task<string> GenerateToken(IdentityUser user);

    IdentityToken? ValidateToken(string tokenString);
}

[Service(typeof(IIdentityTokenService))]
internal class IdentityTokenService : IIdentityTokenService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IHttpContextAccessor _httpContextAccessor;

    static string IdentityTokenSalt;
    static string IdentityTokenKey;

    static IdentityTokenService()
    {
        IdentityTokenSalt = RandomString(length: 12);
        IdentityTokenKey = RandomString(length: 12);
    }

    public IdentityTokenService(IServiceProvider serviceProvider, IHttpContextAccessor httpContextAccessor)
    {
        _serviceProvider = serviceProvider;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<string> GenerateToken(IdentityUser user)
    {
        var token = await BuildToken(user);
        var tokenString = EncryptToken(token);
        return tokenString;
    }

    public IdentityToken? ValidateToken(string tokenString)
    {
        var token = DecryptToken(tokenString);
        if (token == null)
            return null;

        if (token.Signature != Signature(token))
            return null;

        //if (token.Validity.ValidFromIPAddress != RemoteIpAddress)
        //    return null;

        // TODO: Extend the token if necessary?
        if (token.Validity.ValidUntil < DateTimeOffset.UtcNow)
            return null;

        return token;
    }

    // Helpers

    async Task<IdentityToken> BuildToken(IdentityUser user, TimeSpan? validFor = null)
    {
        if (validFor == null)
            validFor = TimeSpan.FromHours(6); // Default token validity of 6 hours

        var token = new IdentityToken
        {
            User = user,
            Claims = new IdentityClaims(),
            Validity = new IdentityValidity(),
            Signature = string.Empty
        };

        token.Validity.ValidUntil = DateTimeOffset.UtcNow.Add(validFor.Value);
        //token.Validity.ValidFromIPAddress = RemoteIpAddress;

        foreach (var identityClaimsProvider in _serviceProvider.GetServices<IIdentityClaimsProvider>())
            await identityClaimsProvider.ProvideIdentityClaims(token.User, token.Claims);
        token.Claims.Sanitize();

        token.Signature = Signature(token);

        return token;
    }

    string Signature(IdentityToken token)
    {
        return Hash(Stringify(token));
    }

    string Stringify(IdentityToken token)
    {
        var sb = new StringBuilder();
        sb.Append(JsonSerializer.Serialize<IdentityUser>(token.User));
        sb.Append(JsonSerializer.Serialize<IdentityClaims>(token.Claims));
        sb.Append(JsonSerializer.Serialize<IdentityValidity>(token.Validity));
        sb.Append(IdentityTokenSalt);
        return sb.ToString();
    }

    string Hash(string input)
    {
        if (String.IsNullOrEmpty(input))
            return "EMPTY";
        var inputBytes = Encoding.UTF8.GetBytes(input);
        var hashAlgorithm = SHA256.Create();
        var hashedBytes = hashAlgorithm.ComputeHash(inputBytes);
        return Convert.ToBase64String(hashedBytes);
    }

    string RemoteIpAddress
    {
        get
        {
            return _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "::1";
        }
    }

    string EncryptToken(IdentityToken token)
    {
        return Encrypt(JsonSerializer.Serialize(token), IdentityTokenKey);
    }

    IdentityToken? DecryptToken(string cipherText)
    {
        try
        {
            return JsonSerializer.Deserialize<IdentityToken>(Decrypt(cipherText, IdentityTokenKey));
        }
        catch
        {
            return null;
        }
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
        using (var keyDerivationFunction = new Rfc2898DeriveBytes(key, saltSize, 2, HashAlgorithmName.SHA256))
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

        using (var keyDerivationFunction = new Rfc2898DeriveBytes(key, saltBytes, 2, HashAlgorithmName.SHA256))
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

    static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!£$%^&*()_-";
        var stringChars = new char[length];

        for (int i = 0; i < stringChars.Length; i++)
            stringChars[i] = chars[RandomNumberGenerator.GetInt32(chars.Length)];

        var finalString = new String(stringChars);
        return finalString;
    }
}
