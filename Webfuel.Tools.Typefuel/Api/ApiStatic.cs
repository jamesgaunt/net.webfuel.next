using System;
using System.Collections.Generic;
using System.Text;

namespace Webfuel.Tools.Typefuel
{
    public class ApiStatic
    {
        public ApiSchema Schema;

        public ApiStatic(ApiSchema schema)
        {
            Schema = schema;
        }

        public bool IsEnum { get; set; }

        public string Name { get; set; }

        public List<ApiStaticRow> Rows { get; } = new List<ApiStaticRow>();

        public object[] ValueArray { get; set; }

        public ApiTypeDescriptor ValueType { get; set; }
    }

    public class ApiStaticRow
    {
        public ApiStatic StaticData;

        public ApiStaticRow(ApiStatic staticData)
        {
            StaticData = staticData;
        }

        public string Name { get; set; }

        public object Value { get; set; }
    }
}
