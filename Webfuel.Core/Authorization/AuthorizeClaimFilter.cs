using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel
{
    public static class AuthorizeClaimExtensions
    {
        public static RouteHandlerBuilder AuthorizeClaim(this RouteHandlerBuilder builder, Func<IdentityClaims, bool> claim)
        {
            builder.AddEndpointFilter((EndpointFilterInvocationContext context, EndpointFilterDelegate next) =>
            {
                var token = context.HttpContext.GetState<IdentityToken>(IdentityToken.Key);
                if (token == null || !claim(token.Claims))
                    throw new UnauthorizedAccessException();

                return next(context);
            });

            return builder;
        }
    }
}
