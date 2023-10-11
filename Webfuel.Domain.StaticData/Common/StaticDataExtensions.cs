using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain.StaticData
{
    public static class StaticDataExtensions
    {
        public static T? GetDefault<T>(this IReadOnlyList<T> items) where T : class, IStaticData
        {
            if (items.Count == 0)
                return null;

            var item = items.FirstOrDefault(p => p.Default);
            if (item != null)
                return item;

            return items[0];
        }
        public static T RequireDefault<T>(this IReadOnlyList<T> items) where T : class, IStaticData
        {
            return items.GetDefault() ?? throw new InvalidOperationException($"No static data defined for {nameof(T)}");
        }

        public static T? First<T>(this IReadOnlyList<T> items, Guid? id) where T : class, IStaticData
        {
            return items.FirstOrDefault(p => p.Id == id);
        }

        public static T GetOrDefault<T>(this IReadOnlyList<T> items, Guid? id) where T : class, IStaticData
        {
            return items.FirstOrDefault(p => p.Id == id) ?? RequireDefault(items);
        }
    }
}
