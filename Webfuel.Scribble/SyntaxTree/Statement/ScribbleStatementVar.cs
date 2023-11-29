using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Scribble
{
    public class ScribbleStatementVar: ScribbleStatement
    {
        private ScribbleStatementVar(IReadOnlyList<ScribbleExprArgument> variables)
        {
            Variables = variables;
        }

        public IReadOnlyList<ScribbleExprArgument> Variables { get; private set; }

        public static ScribbleStatementVar Create(IEnumerable<ScribbleExprArgument> variables)
        {
            return new ScribbleStatementVar(variables.ToList());
        }

        public override string ToString()
        {
            return "var " + String.Join(", ", Variables.Select(v => $"{v.Identifier?.Name} = {v.Expr}")) + ";";
        }

        List<Type> VariableTypes = new List<Type>();

        internal override void Bind<T>(ScribbleBindingContext<T> context)
        {
            foreach (var variable in Variables)
            {
                var valueType = variable.Expr.Bind(context);
                if (valueType == Reflection.UnknownType)
                    throw new InvalidOperationException("Unable to resove type of expression: " + variable.Expr);
                var name = variable.Identifier?.Name ?? String.Empty;
                context.Scope.DeclareVar(name, valueType, null);
                VariableTypes.Add(valueType);
            }
        }

        internal override async Task ExecuteAsync<T>(ScribbleExecutionContext<T> context)
        {
            for(var i = 0; i < Variables.Count; i++)
            {
                var value = await context.EvaluateExpressionAsync(Variables[i].Expr);
                var type = VariableTypes[i];
                var name = Variables[i].Identifier?.Name ?? String.Empty;
                context.Scope.DeclareVar(name, type, value);
            }
        }
    }
}
