using System;
using System.Collections.Generic;
using System.Text;

namespace Webfuel.Tools.Typefuel
{
    public class ApiEnum
    {
        public ApiSchema Schema;

        public ApiEnum(ApiSchema schema)
        {
            Schema = schema;
        }

        public bool IsEnum { get; set; } // If it's not an enum then it's a static class

        public string Name { get; set; }

        public List<ApiEnumRow> Rows { get; } = new List<ApiEnumRow>();

        public object[] ValueArray { get; set; }

        public ApiTypeDescriptor ValueType { get; set; }
    }

    public class ApiEnumRow
    {
        public ApiEnum Enum;

        public ApiEnumRow(ApiEnum @enum)
        {
            Enum = @enum;
        }

        public string Name { get; set; }

        public object Value { get; set; }
    }
}
