using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Webfuel
{
    public static class HttpContextExtensions
    {
        public static T GetOrCreateState<T>(this IHttpContextAccessor httpContextAccessor, string key) where T: class, new()
        {
            return httpContextAccessor.GetOrCreateState<T>(key, () => new T());
        }

        public static T GetOrCreateState<T>(this IHttpContextAccessor httpContextAccessor, string key, Func<T> generator) where T: class
        {
            if (httpContextAccessor == null)
                return generator();

            var httpContext = httpContextAccessor.HttpContext;
            if (httpContext == null)
                return generator();

            if(!httpContext.Items.ContainsKey(key) || !(httpContext.Items[key] is T))
                httpContext.Items[key] = generator();

            return (T)httpContext.Items[key]!;    
        }

        public static T? GetState<T>(this IHttpContextAccessor httpContextAccessor, string key) where T : class
        {
            if (httpContextAccessor == null)
                return null;

            var httpContext = httpContextAccessor.HttpContext;
            if (httpContext == null)
                return null;

            if (!httpContext.Items.ContainsKey(key) || !(httpContext.Items[key] is T))
                return null;

            return httpContext.Items[key] as T;
        }

        public static void SetState<T>(this IHttpContextAccessor httpContextAccessor, string key, T state) where T : class
        {
            if (httpContextAccessor == null)
                return;

            var httpContext = httpContextAccessor.HttpContext;
            if (httpContext == null)
                return;

            httpContext.Items[key] = state;
        }
    }
}
