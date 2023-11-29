using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Webfuel.Scribble
{
    public class ScribbleExprIdentifier : ScribbleExpr
    {
        private ScribbleExprIdentifier(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public override string ToString()
        {
            return Name;
        }

        public static ScribbleExprIdentifier Create(ReadOnlySpan<char> name)
        {
            var hash = StringUtility.Hash(name);

            if (Pool.ContainsKey(hash))
            {
                var poolNode = Pool[hash];
                if (name.CompareTo(poolNode.Name, StringComparison.Ordinal) == 0)
                    return poolNode;
                return new ScribbleExprIdentifier(name.ToString());
            }
            return Pool[hash] = new ScribbleExprIdentifier(name.ToString());
        }

        static ConcurrentDictionary<Int32, ScribbleExprIdentifier> Pool = new ConcurrentDictionary<int, ScribbleExprIdentifier>();

        internal override Type Bind<T>(ScribbleBindingContext<T> context)
        {
            return context.IdentifierType(Name, set: false, get: true);
        }

        internal override IEnumerable<ScribbleExpr> GetPrerequisites<T>(ScribbleExecutionContext<T> context)
        {
            yield break;
        }

        internal override object? Evaluate<T>(ScribbleExecutionContext<T> context, List<object?> prerequisites)
        {
            // This is only used when the identifier is on global scope.
            return context.GetProperty(Name);
        }
    }
}
