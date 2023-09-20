using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Webfuel
{
    public interface IIdentityTokenService
    {
        void ActivateToken(IdentityToken token, TimeSpan? validFor = null);

        bool ValidateToken(IdentityToken token);
    }

    [ServiceImplementation(typeof(IIdentityTokenService))]
    internal class IdentityTokenService: IIdentityTokenService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IdentityTokenService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void ActivateToken(IdentityToken token, TimeSpan? validFor = null)
        {
            if (validFor == null)
                validFor = TimeSpan.FromHours(6); // Default token validity of 6 hours

            token.Validity.ValidUntil = DateTimeOffset.UtcNow.Add(validFor.Value);
            token.Validity.ValidFromIPAddress = RemoteIpAddress;
            token.Signature = Signature(token);
        }

        public bool ValidateToken(IdentityToken token)
        {
            if (token.Signature != Signature(token))
                return false;

            if (token.Validity.ValidUntil < DateTimeOffset.UtcNow)
                return false;

            if (token.Validity.ValidFromIPAddress != RemoteIpAddress)
                return false;

            return true;
        }

        // Helpers

        string Stringify(IdentityToken token)
        {
            var sb = new StringBuilder();
            sb.Append(JsonSerializer.Serialize<IdentityUser>(token.User));
            sb.Append(JsonSerializer.Serialize<IdentityClaims>(token.Claims));
            sb.Append(JsonSerializer.Serialize<IdentityValidity>(token.Validity));
            return sb.ToString();
        }

        string Signature(IdentityToken token)
        {
            return Hash(Stringify(token));
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
    }
}
