using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Webfuel.Scribble.Extensions
{
    public static class ScribbleEnumerableExtensions
    {
        public static async Task<List<T>> Where<T>(this IEnumerable<T> collection, ScribbleFunc<T, bool> predicate)
        {
            var result = new List<T>();
            foreach(var item in collection)
            {
                if (await predicate.InvokeAsync(item))
                    result.Add(item);
            }
            return result;
        }

        public static async Task<int> Count<T>(this IEnumerable<T> collection, ScribbleFunc<T, bool> predicate)
        {
            var items = await Where(collection, predicate);
            return items.Count;
        }

        public static async Task<List<R>> Map<T, R>(this IEnumerable<T> collection, ScribbleFunc<T, R> predicate)
        {
            var result = new List<R>();
            foreach (var item in collection)
            {
                result.Add(await predicate.InvokeAsync(item));
            }
            return result;
        }

        public static async Task<int> Sum<T>(this IEnumerable<T> collection, ScribbleFunc<T, int> predicate)
        {
            int result = 0;
            foreach(var item in collection)
            {
                result += await predicate.InvokeAsync(item);
            }
            return result;
        }

        public static async Task<decimal> Sum<T>(this IEnumerable<T> collection, ScribbleFunc<T, decimal> predicate)
        {
            decimal result = 0;
            foreach (var item in collection)
            {
                result += await predicate.InvokeAsync(item);
            }
            return result;
        }
    }
}
