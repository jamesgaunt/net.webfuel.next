using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel
{
    public static class ChangeLogUtility
    {
        public class ListComparison<T>
        {
            public List<T> Added { get; } = new List<T>();
            public List<T> Removed { get; } = new List<T>();
        }

        public static ListComparison<T>? CompareLists<T>(IEnumerable<T> original, IEnumerable<T> updated)
        {
            ListComparison<T>? result = null;
            foreach (var u in updated)
            {
                if (!original.Contains(u))
                    (result ??= new ListComparison<T>()).Added.Add(u);
            }
            foreach (var o in original)
            {
                if (!updated.Contains(o))
                    (result ??= new ListComparison<T>()).Removed.Add(o);
            }
            return result;
        }
    }
}
