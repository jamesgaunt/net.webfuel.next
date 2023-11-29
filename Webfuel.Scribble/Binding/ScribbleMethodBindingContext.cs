using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Webfuel.Scribble
{
    internal class ScribbleMethodBindingContext
    {
        public ScribbleMethodBindingContext(Type targetType, string name, ScribbleArgInfo[] args, IReadOnlyList<ScribbleTypeName>? typeArguments, Func<Type[], Type>? lambdaTypeResolver)
        {
            TargetType = targetType;
            TypeInfo = Reflection.GetTypeInfo(TargetType);
            Name = name;
            Args = args;
            TypeArguments = typeArguments;
            LambdaTypeResolver = lambdaTypeResolver;
            DoNotCache = false;
        }

        public Type TargetType { get; }

        public ScribbleTypeInfo TypeInfo { get; }

        public string Name { get; }

        public ScribbleArgInfo[] Args { get; }

        public IReadOnlyList<ScribbleTypeName>? TypeArguments { get; }

        public Func<Type[], Type>? LambdaTypeResolver { get; }

        public bool DoNotCache { get; set;  }

        public string CacheKey
        {
            get
            {
                var sb = new StringBuilder($"{TargetType.FullName}.{Name}");

                if (TypeArguments != null)
                    sb.Append($"<{String.Join(",", TypeArguments.Select(p => p.ToString()))}>");

                sb.Append("(");
                foreach (var arg in Args)
                {
                    if (arg.Name != null)
                        sb.Append($"{arg.Name}:");
                    sb.Append(arg.ArgumentType.FullName);
                    sb.Append(",");
                }
                sb.Append(")");

                var key = sb.ToString();
                return key;
            }
        }
    }
}
