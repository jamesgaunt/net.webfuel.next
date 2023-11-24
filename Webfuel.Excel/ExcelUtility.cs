using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Excel
{
    internal class ExcelColumn<T>
    {
        public ExcelColumn(string name, int index, PropertyInfo propertyInfo)
        {
            Name = name;
            Index = index;
            PropertyInfo = propertyInfo;
        }

        public string Name { get; set; }

        public int Index { get; set; }

        PropertyInfo PropertyInfo { get; set; }

        public object? GetValue(T item)
        {
            return PropertyInfo.GetValue(item);
        }
    }

    internal static class ExcelUtility
    {
        public static List<ExcelColumn<T>> GenerateColumns<T>()
        {
            var result = new List<ExcelColumn<T>>();

            foreach(var propertyInfo in typeof(T).GetProperties())
            {
                var attribute = propertyInfo.GetCustomAttribute<ExcelPropertyAttribute>();
                if (attribute?.Ignore == true)
                    continue;

                var name = attribute?.Name ?? propertyInfo.Name;
                var sortOrder = attribute?.SortOrder ?? 0;

                result.Add(new ExcelColumn<T>(name, sortOrder, propertyInfo));
            }

            if (result.Count == 0)
                return result;

            // Normalize Index
            { 
                var maxSortOrder = result.Max(p => p.Index);
                foreach(var header in result)
                {
                    if(header.Index == 0)
                    {
                        maxSortOrder++;
                        header.Index = maxSortOrder;
                    }
                }
                result = result.OrderBy(p => p.Index).ToList();
                for(var i = 0; i < result.Count; i++)
                {
                    result[i].Index = i + 1;
                }
            }
            return result;
        }
    }
}
