using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Scribble
{
    public class ScribbleIfPart
    {
        internal ScribbleIfPart(ScribbleExpr condition, ScribbleStatement statement)
        {
            Condition = condition;
            Statement = statement;
        }

        public ScribbleExpr Condition { get; }

        public ScribbleStatement Statement { get; }
    }

    public class ScribbleStatementIf: ScribbleStatement
    {
        private ScribbleStatementIf(IEnumerable<ScribbleIfPart> parts, ScribbleStatement? @default)
        {
            Parts = parts;
            Default = @default;
        }

        public IEnumerable<ScribbleIfPart> Parts { get; }

        public ScribbleStatement? Default { get; }

        public static ScribbleStatementIf Create(IEnumerable<ScribbleIfPart> parts, ScribbleStatement? @default)
        {
            return new ScribbleStatementIf(parts, @default);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach(var part in Parts)
            {
                sb.Append(sb.Length == 0 ? "if " : " else if ");
                sb.Append("(" + part.Condition.ToString() + ") ");
                sb.Append(part.Statement.ToString());
            }
            if(Default != null)
            {
                sb.Append(" else ");
                sb.Append(Default.ToString());
            }
            return sb.ToString();
        }

        internal override void Bind<T>(ScribbleBindingContext<T> context)
        {
            foreach (var part in Parts)
            {
                var resultType = part.Condition.Bind(context);
                if (resultType != typeof(Boolean))
                    throw new InvalidOperationException("if statement expressions must be boolean");

                part.Statement.Bind(context);
            }
            if (Default != null)
                Default.Bind(context);
        }

        internal override async Task ExecuteAsync<T>(ScribbleExecutionContext<T> context)
        {
            foreach (var part in Parts)
            {
                var result = (bool)(await context.EvaluateExpressionAsync(part.Condition))!;

                if (result)
                {
                    await part.Statement.ExecuteAsync(context);
                    return;
                }
            }

            // We fell through all conditions
            if (Default != null)
                await Default.ExecuteAsync(context);
        }
    }
}
