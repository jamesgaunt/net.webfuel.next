using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Webfuel.Scribble
{
    public class ScribbleTypeName : ScribbleNode
    {
        private ScribbleTypeName(string name, IReadOnlyList<ScribbleTypeName>? arguments = null)
        {
            Name = name;
            Arguments = arguments ?? EmptyArguments;
        }

        public string Name { get; private set; }

        public IReadOnlyList<ScribbleTypeName> Arguments { get; private set; }

        public override string ToString()
        {
            var sb = new StringBuilder(Name);
            if(Arguments.Count > 0)
                sb.Append($"<{String.Join(", ", Arguments.Select(p => p.ToString()))}>");
            return sb.ToString();
        }

        public static ScribbleTypeName Create(string name, IReadOnlyList<ScribbleTypeName>? arguments = null)
        {
            name = Reflection.FixConstructableTypeName(name);
            return new ScribbleTypeName(name.ToString(), arguments);
        }

        public override bool Equals(object? obj)
        {
            var other = obj as ScribbleTypeName;
            if (other == null)
                return false;

            if (other.Name != Name || other.Arguments.Count != Arguments.Count)
                return false;

            for (var i = 0; i < Arguments.Count; i++)
            {
                if (other.Arguments[i] != Arguments[i])
                    return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            var hash = Name.GetHashCode();
            foreach (var argument in Arguments)
                hash = HashCode.Combine(hash, argument.GetHashCode());
            return hash;
        }

        private static IReadOnlyList<ScribbleTypeName> EmptyArguments = new List<ScribbleTypeName>();
    }
}
