using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webfuel
{
    public class StringResult
    {
        public StringResult()
        {
        }

        public StringResult(string value)
        {
            Value = value;
        }

        public StringResult(object value)
        {
            if (value != null)
                Value = value.ToString();
        }

        public string? Value { get; set; }
    }
}
