using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Scribble
{
    public class ScribbleTemplate<T>
    {
        internal IReadOnlyList<ScribbleStatement> Statements { get; }

        internal IReadOnlyList<ScribbleExprInvoke> AsyncValidatorInvokes { get; }

        internal ScribbleTemplate(IEnumerable<ScribbleStatement> statements, IEnumerable<ScribbleExprInvoke> asyncValidatorInvokes)
        {
            Statements = statements.ToList();
            AsyncValidatorInvokes = asyncValidatorInvokes.ToList();
        }


        public async Task<object?> RenderAsync(T env)
        {
            // Script is empty
            if (Statements.Count == 0)
                return String.Empty;

            // Script is a plain text template
            if (Statements.Count == 1 && Statements[0] is ScribbleStatementWriteText)
                return ((ScribbleStatementWriteText)Statements[0]).Text;

            var context = new ScribbleExecutionContext<T>(env);
            foreach (var statement in Statements)
            {
                if (context.ExecutionState != ScribbleExecutionState.Executing)
                    return context.ReturnValue;

                await statement.ExecuteAsync(context);
            }

            // If the script hasn't returned a value, it returns the value of its writer
            if (context.ExecutionState == ScribbleExecutionState.Executing)
                context.Return(context.Writer.ToString());

            return context.ReturnValue;
        }

        public async Task ValidateAsync(object? validationContext)
        {
            foreach (var item in AsyncValidatorInvokes)
            {
                var result = await item.ValidateAsync(validationContext);
                if (!String.IsNullOrEmpty(result))
                    throw new InvalidOperationException(result);
            }
        }
    }
}
