using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

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
            if (token != null)
                context.SetState(IdentityToken.Key, token);

            await _request(context);
        }

        IdentityToken? GetIdentityToken(HttpContext context)
        {
            var jsonToken = ReadHeaderJson(context);
            if (String.IsNullOrEmpty(jsonToken))
                return null;
            
            var token = JsonSerializer.Deserialize<IdentityToken>(jsonToken);
            if (token == null)
                return null;

            if (!_identityTokenService.ValidateToken(token))
                return null;

            return token;
        }

        string? ReadHeaderJson(HttpContext httpContext)
            
        {
            if (!httpContext.Request.Headers.ContainsKey(IdentityToken.Key))
                return null;

            var header = httpContext.Request.Headers[IdentityToken.Key];
            if (header.Count == 0)
                return null;

            var token = header[0];
            if (String.IsNullOrEmpty(token))
                return null;

            return token;
        }
    }
}
