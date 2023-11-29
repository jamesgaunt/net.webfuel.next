using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webfuel.Scribble
{
    static class ScribblePrefixOpBinder
    {
        public static object? EvaluatePrefixOp(ScribblePrefixOp op, object? rhs)
        {
            switch (op)
            {
                case ScribblePrefixOp.Minus:
                    return -(dynamic)rhs!;

                case ScribblePrefixOp.Plus:
                    return +(dynamic)rhs!;

                case ScribblePrefixOp.Not:
                    return !(dynamic)rhs!;
            }
            throw new InvalidOperationException("Unsupported prefix operation");
        }

        public static Type ResolvePrefixOp(ScribblePrefixOp op, Type rhs)
        {
            switch (op)
            {
                case ScribblePrefixOp.Plus:
                case ScribblePrefixOp.Minus:
                    if (Reflection.IsNumeric(rhs))
                        return rhs;
                    return Reflection.UnknownType;

                case ScribblePrefixOp.Not:
                    if (Reflection.IsBoolean(rhs))
                        return rhs;
                    return Reflection.UnknownType;
            }

            return Reflection.UnknownType;
        }
    }
}
