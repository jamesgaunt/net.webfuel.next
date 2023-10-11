using Microsoft.AspNetCore.Http;

namespace Webfuel
{
    public interface IIdentityAccessor
    {
        IdentityUser? User { get; }

        IdentityClaims Claims { get; }
    }

    [Service(typeof(IIdentityAccessor))]
    internal class IdentityAccessor : IIdentityAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IdentityAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IdentityUser? User
        {
            get
            {
                var token = _httpContextAccessor.HttpContext?.GetState<IdentityToken>(IdentityToken.Key);
                if (token == null)
                    return null;
                return token.User;
            }
        }

        public IdentityClaims Claims
        {
            get
            {
                var token = _httpContextAccessor.HttpContext?.GetState<IdentityToken>(IdentityToken.Key);
                if (token == null)
                    return new IdentityClaims();
                return token.Claims;
            }
        }
    }
}
