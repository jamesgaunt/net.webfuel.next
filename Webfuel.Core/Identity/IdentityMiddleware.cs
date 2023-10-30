using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel
{
    public class IdentityMiddleware
    {
        private readonly RequestDelegate _request;
        private readonly IIdentityTokenService _identityTokenService;

        public IdentityMiddleware(RequestDelegate request, IIdentityTokenService identityTokenService)
        {
            _request = request;
            _identityTokenService = identityTokenService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = GetIdentityToken(context);
            if (token != null) { 
                context.SetState(IdentityStatic.StateKey, token);
            }
            await _request(context);
        }

        IdentityToken? GetIdentityToken(HttpContext context)
        {
            var tokenString = ReadHeaderJson(context);
            if (String.IsNullOrEmpty(tokenString))
                return null;

            return _identityTokenService.ValidateToken(tokenString);
        }

        string? ReadHeaderJson(HttpContext httpContext)
        {
            if (!httpContext.Request.Headers.ContainsKey(IdentityStatic.Header))
                return null;

            var header = httpContext.Request.Headers[IdentityStatic.Header];
            if (header.Count == 0)
                return null;

            var token = header[0];
            if (String.IsNullOrEmpty(token))
                return null;

            return token;
        }
    }
}
