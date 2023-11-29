using System;
using System.Collections.Generic;
using System.Text;

namespace Webfuel.Scribble
{ 
    internal class ScribbleScope
    {
        private readonly Dictionary<string, ScribbleVar> Variables = new Dictionary<string, ScribbleVar>();

        public void DeclareVar(string name, Type type, object? value)
        {
            if (Variables.ContainsKey(name))
                throw new InvalidOperationException($"A local variable named '{name}' is already declared in this scope");
            Variables[name] = new ScribbleVar(type, name, value);
        }

        public ScribbleVar? GetVar(string name)
        {
            if (!Variables.ContainsKey(name))
                return null;
            return Variables[name];
        }
    }
}
