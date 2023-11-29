using System;
using System.Collections.Generic;
using System.Text;

namespace Webfuel.Scribble
{
    internal class ScribbleScopeStack
    {
        private List<ScribbleScope> Stack = new List<ScribbleScope>();

        public void PushScope()
        {
            Stack.Add(new ScribbleScope());
        }

        public void PopScope()
        {
            if (Stack.Count == 0)
                throw new InvalidOperationException("Attempted to pop scope from an empty stack");

            Stack.RemoveAt(Stack.Count - 1);
        }

        public void DeclareVar(string name, Type type, object? value)
        {
            if (Stack.Count == 0)
                PushScope();
            Stack[Stack.Count - 1].DeclareVar(name, type, value);
        }

        public ScribbleVar? GetVar(string name)
        {
            for (var i = Stack.Count - 1; i >= 0; i--)
            {
                var var = Stack[i].GetVar(name);
                if (var != null)
                    return var;
            }
            return null;
        }
    }
}
