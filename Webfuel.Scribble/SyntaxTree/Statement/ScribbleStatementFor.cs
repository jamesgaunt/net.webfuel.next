using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Scribble
{
    public class ScribbleStatementFor: ScribbleStatement
    {
        private ScribbleStatementFor(ScribbleStatement initialiser, ScribbleExpr condition, ScribbleExpr iterator, ScribbleStatement body)
        {
            Initialiser = initialiser;
            Condition = condition;
            Iterator = iterator;
            Body = body;
        }

        public ScribbleStatement Initialiser { get; }

        public ScribbleExpr Condition { get; }

        public ScribbleExpr Iterator { get; }

        public ScribbleStatement Body { get; }

        public static ScribbleStatementFor Create(ScribbleStatement initialiser, ScribbleExpr condition, ScribbleExpr iterator, ScribbleStatement body)
        {
            return new ScribbleStatementFor(initialiser, condition, iterator, body);
        }

        public override string ToString()
        {
            return $"for ({Initialiser}; {Condition}; {Iterator}) {Body}";
        }

        internal override void Bind<T>(ScribbleBindingContext<T> context)
        {
            context.Scope.PushScope();
            {
                Initialiser.Bind(context);

                if (Condition.Bind(context) == Reflection.UnknownType)
                    throw new InvalidOperationException("Unable to resove type of expression: " + Condition);

                if (Iterator.Bind(context) == Reflection.UnknownType)
                    throw new InvalidOperationException("Unable to resove type of expression: " + Iterator);

                Body.Bind(context);
            }
            context.Scope.PopScope();
        }

        internal override async Task ExecuteAsync<T>(ScribbleExecutionContext<T> context)
        {
            context.Scope.PushScope();
            {
                await Initialiser.ExecuteAsync(context);
                var result = (bool)(await context.EvaluateExpressionAsync(Condition))!;
                while (result)
                {
                    await Body.ExecuteAsync(context);
                    await context.EvaluateExpressionAsync(Iterator);
                    result = (bool)(await context.EvaluateExpressionAsync(Condition))!;
                }
            }
            context.Scope.PopScope();
        }
    }
}
