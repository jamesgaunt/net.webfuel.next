using System;
using System.Collections.Generic;
using System.Text;

namespace Webfuel.Tools.Typefuel
{
    public class ApiService
    {
        public readonly ApiSchema Schema;

        public ApiService(ApiSchema schema)
        {
            Schema = schema;
        }

        public string Name { get; set; }

        public List<ApiMethod> Methods { get; } = new List<ApiMethod>();
    }
}
