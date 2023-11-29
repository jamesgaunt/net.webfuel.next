using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webfuel.Scribble
{
    static class ScribblePostfixOpBinder
    {
        public static Type ResolvePostfixOp(Type lhs, ScribblePostfixOp op)
        {
            switch (op)
            {
                case ScribblePostfixOp.PostIncrement:
                case ScribblePostfixOp.PostDecrement:
                    if (Reflection.IsNumeric(lhs))
                        return lhs;
                    return Reflection.UnknownType;
            }

            return Reflection.UnknownType;
        }
    }
}
