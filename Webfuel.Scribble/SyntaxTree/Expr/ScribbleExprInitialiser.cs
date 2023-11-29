using System;
using System.Collections.Generic;

namespace Webfuel.Scribble
{
    public class ScribbleInitialiser
    {
        private ScribbleInitialiser(ScribbleExprIdentifier identifier, ScribbleExpr expr)
        {
            Identifier = identifier;
            Expr = expr;
        }

        public ScribbleExprIdentifier Identifier { get; private set; }

        public ScribbleExpr Expr { get; private set; }

        public static ScribbleInitialiser Create(ScribbleExprIdentifier identifier, ScribbleExpr expr)
        {
            return new ScribbleInitialiser(identifier, expr);
        }
    }

    public class ScribbleExprInitialiser : ScribbleNode
    {
        private ScribbleExprInitialiser(IReadOnlyList<ScribbleInitialiser> initialisers)
        {
            Initialisers = initialisers;
        }

        public IReadOnlyList<ScribbleInitialiser> Initialisers { get; private set; }

        public static ScribbleExprInitialiser Create(IReadOnlyList<ScribbleInitialiser> initialisers)
        {
            return new ScribbleExprInitialiser(initialisers);
        }

        public override string ToString()
        {
            return "TODO";
        }
    }
}
