using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Webfuel.Tools.Typefuel
{
    public class ApiAction
    {
        public readonly ApiController Controller;

        public ApiAction(ApiController controller)
        {
            Controller = controller;
        }

        public int RetryCount { get; set; } = -1;

        public string Name { get; set; }

        public string Verb { get; set; }

        public ApiRoute Route { get; set; }

        public List<ApiActionParameter> Parameters { get; } = new List<ApiActionParameter>();

        public ApiActionParameter BodyParameter { get { return Parameters.FirstOrDefault(p => p.Source == ApiActionParameterSource.Body); } }

        public ApiTypeDescriptor CommandTypeDescriptor { get; set; } 

        public ApiTypeDescriptor ReturnTypeDescriptor { get; set; }
    }
}
