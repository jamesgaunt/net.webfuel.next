using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Excel
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ExcelPropertyAttribute: Attribute
    {
        public string Name { get; set; } = String.Empty;

        public int SortOrder { get; set; } = 0;

        public bool Ignore { get; set; }
    }
}
