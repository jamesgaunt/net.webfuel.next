using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel
{
    public static class AuthorizeExtensions
    {
        public static RouteHandlerBuilder RequireClaim(this RouteHandlerBuilder builder, Func<IdentityClaims, bool> claim)
        {
            builder.AddEndpointFilter((EndpointFilterInvocationContext context, EndpointFilterDelegate next) =>
            {
                var token = context.HttpContext.GetState<IdentityToken>(IdentityStatic.StateKey);
                if (token == null)
                    throw new NotAuthenticatedException();
                if (!claim(token.Claims))
                    throw new NotAuthorizedException();

                return next(context);
            });

            return builder;
        }

        public static RouteHandlerBuilder RequireIdentity(this RouteHandlerBuilder builder)
        {
            builder.AddEndpointFilter((EndpointFilterInvocationContext context, EndpointFilterDelegate next) =>
            {
                var token = context.HttpContext.GetState<IdentityToken>(IdentityStatic.StateKey);
                if (token == null)
                    throw new NotAuthenticatedException();

                return next(context);
            });

            return builder;
        }

    }
}
