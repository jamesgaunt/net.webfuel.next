using System;
using System.Collections.Generic;
using System.Text;

namespace Webfuel
{
    public class ValueResult<T>
    { 
        public ValueResult(T value)
        {
            Value = value;
        }

        public T Value { get; set; }
    }
}
