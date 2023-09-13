using System;
using System.Collections.Generic;
using System.Text;

namespace Webfuel.Tools.Typefuel
{
    public class ApiActionParameter
    {
        public ApiAction Action;

        public ApiActionParameter(ApiAction action)
        {
            Action = action;
        }

        public string Name { get; set; }

        public ApiTypeDescriptor TypeDescriptor { get; set; }

        public ApiActionParameterSource Source { get; set; }
    }

    public enum ApiActionParameterSource
    {
        Unknown = 0,
        Route = 1,
        Body = 2,
    }
}
