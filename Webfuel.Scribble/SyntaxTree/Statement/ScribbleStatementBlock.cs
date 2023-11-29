using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Scribble
{
    public class ScribbleStatementBlock: ScribbleStatement
    {
        private ScribbleStatementBlock(IEnumerable<ScribbleStatement> statements)
        {
            Statements = statements;
        }

        public IEnumerable<ScribbleStatement> Statements { get; }

        public static ScribbleStatementBlock Create(IEnumerable<ScribbleStatement> statements)
        {
            return new ScribbleStatementBlock(statements);
        }

        public static ScribbleStatementBlock Create(ScribbleStatement statement)
        {
            return new ScribbleStatementBlock(new List<ScribbleStatement> { statement });
        }

        public override string ToString()
        {
            var sb = new StringBuilder("{ ");
            foreach (var statement in Statements)
                sb.Append(statement.ToString() + " ");
            sb.Append("}");
            return sb.ToString();
        }

        internal override void Bind<T>(ScribbleBindingContext<T> context)
        {
            context.Scope.PushScope();
            {
                foreach (var statement in Statements)
                    statement.Bind(context);
            }
            context.Scope.PopScope();
        }

        internal override async Task ExecuteAsync<T>(ScribbleExecutionContext<T> context)
        {
            context.Scope.PushScope();
            {
                foreach (var statement in Statements)
                {
                    if (context.ExecutionState != ScribbleExecutionState.Executing)
                        return;

                    await statement.ExecuteAsync(context);
                }
            }
            context.Scope.PopScope();
        }
    }
}
