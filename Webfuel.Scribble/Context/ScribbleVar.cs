using System;
using System.Collections.Generic;
using System.Text;

namespace Webfuel.Scribble
{ 
    internal class ScribbleVar
    {
        public ScribbleVar(Type type, string? name, object? value = null)
        {
            Type = type;
            Name = name;
            Value = value;
        }

        public Type Type { get; }

        public string? Name { get; }

        public object? Value { get; set; }
    }
}
