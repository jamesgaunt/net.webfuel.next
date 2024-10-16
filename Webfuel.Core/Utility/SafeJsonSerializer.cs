using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Webfuel
{
    public static class SafeJsonSerializer
    {
        public static T Deserialize<T>(string json) where T : class, new()
        {
            if (String.IsNullOrEmpty(json))
                return new T();

            try
            {
                return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new T();
            }
            catch
            {
                return new T();
            }
        }

        public static string Serialize<T>(T item) where T : class, new()
        {
            try
            {
                return JsonSerializer.Serialize(item, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }) ?? "{}";
            }
            catch
            {
                return "{}";
            }
        }

        public static string Cycle<T>(string json) where T : class, new()
        {
            return Serialize(Deserialize<T>(json));
        }
    }
}
