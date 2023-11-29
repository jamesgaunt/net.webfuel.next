using System;

namespace Webfuel.Scribble
{
    public class ScribbleExprArgument : ScribbleNode
    {
        private ScribbleExprArgument(ScribbleExprIdentifier? identifier, ScribbleExpr expr)
        {
            Identifier = identifier;
            Expr = expr;
        }

        public ScribbleExprIdentifier? Identifier { get; private set; }

        public ScribbleExpr Expr { get; private set; }

        public static ScribbleExprArgument Create(ScribbleExprIdentifier? identifier, ScribbleExpr expr)
        {
            return new ScribbleExprArgument(identifier, expr);
        }

        public override string ToString()
        {
            if (Identifier != null)
                return $"{Identifier}: {Expr}";
            return Expr.ToString()!;
        }
    }
}
