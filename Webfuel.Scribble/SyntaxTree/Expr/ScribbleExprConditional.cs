using System;
using System.Collections.Generic;

namespace Webfuel.Scribble
{
    public class ScribbleExprConditional : ScribbleExpr
    {
        private ScribbleExprConditional(ScribbleExpr condition, ScribbleExpr truePart, ScribbleExpr falsePart)
        {
            Condition = condition;
            TruePart = truePart;
            FalsePart = falsePart;
        }

        public ScribbleExpr Condition { get; private set; }

        public ScribbleExpr TruePart { get; private set; }

        public ScribbleExpr FalsePart { get; private set; }

        public static ScribbleExprConditional Create(ScribbleExpr condition, ScribbleExpr truePart, ScribbleExpr falsePart)
        {
            return new ScribbleExprConditional(condition, truePart, falsePart);
        }

        public override string ToString()
        {
            return $"({Condition} ? {TruePart} : {FalsePart})";
        }

        internal override Type Bind<T>(ScribbleBindingContext<T> context)
        {
            var conditionType = Condition.Bind(context);
            if (!Reflection.IsBoolean(conditionType))
                return Reflection.UnknownType;

            var trueType = TruePart.Bind(context);
            var falseType = FalsePart.Bind(context);
            if (trueType == falseType)
                return trueType;

            return Reflection.UnknownType;
        }

        internal override IEnumerable<ScribbleExpr> GetPrerequisites<T>(ScribbleExecutionContext<T> context)
        {
            // Note there is an edge case hack in the runtime to enable short circuiting of the conditional expressions
            return new List<ScribbleExpr> { Condition, TruePart, FalsePart };
        }

        internal override object? Evaluate<T>(ScribbleExecutionContext<T> context, List<object?> prerequisites)
        {
            // Note there is an edge case hack in the runtime to enable short circuiting of the conditional expressions
            return prerequisites[0];
        }
    }
}
