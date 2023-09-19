using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Webfuel
{
    public static class HttpContextExtensions
    {
        public static T GetOrCreateState<T>(this HttpContext httpContext, string key) where T: class, new()
        {
            return httpContext.GetOrCreateState<T>(key, () => new T());
        }

        public static T GetOrCreateState<T>(this HttpContext httpContext, string key, Func<T> generator) where T: class
        {
            if(!httpContext.Items.ContainsKey(key) || !(httpContext.Items[key] is T))
                httpContext.Items[key] = generator();

            return (T)httpContext.Items[key]!;    
        }
       
        public static T? GetState<T>(this HttpContext httpContext, string key) where T : class
        {
            if (!httpContext.Items.ContainsKey(key) || !(httpContext.Items[key] is T))
                return null;

            return httpContext.Items[key] as T;
        }

        public static void SetState<T>(this HttpContext httpContext, string key, T state) where T : class
        {
            httpContext.Items[key] = state;
        }
    }
}
