using System;
using System.Collections.Generic;
using System.Text;

namespace Webfuel.Tools.Typefuel
{
    public class ApiController
    {
        public readonly ApiSchema Schema;

        public ApiController(ApiSchema schema)
        {
            Schema = schema;
        }

        public string Name { get; set; }

        public List<ApiAction> Actions { get; } = new List<ApiAction>();
    }
}
