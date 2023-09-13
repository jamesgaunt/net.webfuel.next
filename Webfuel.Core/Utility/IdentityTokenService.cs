using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel
{
    public interface IIdentityTokenService
    {
        string GenerateToken(Guid identityId, DateTime expiryUtc, string ipAddress);

        Guid ValidateToken(string token, string ipAddress);
    }

    [ServiceImplementation(typeof(IIdentityTokenService))]
    class IdentityTokenService : IIdentityTokenService
    {
        private readonly ITimeLimitedTokenService TimeLimitedTokenService;

        public IdentityTokenService(ITimeLimitedTokenService timeLimitedTokenService)
        {
            TimeLimitedTokenService = timeLimitedTokenService;
        }

        public string GenerateToken(Guid identityId, DateTime expiryUtc, string ipAddress)
        {
            if(ipAddress.StartsWith("::"))
                ipAddress = "127.0.0.1";

            return TimeLimitedTokenService.EncodeToken(content: identityId.ToString(), key: ipAddress, expiryUtc: expiryUtc);
        }

        public Guid ValidateToken(string token, string ipAddress)
        {
            if (ipAddress.StartsWith("::"))
                ipAddress = "127.0.0.1";

            try
            {
                var content = TimeLimitedTokenService.DecodeToken(token: token, key: ipAddress);

                if (!Guid.TryParse(content, out var identityId))
                    return Guid.Empty;

                return identityId;
            }
            catch
            {
                return Guid.Empty;
            }
        }
    }
}
