using System.Text.Json.Serialization;
using Webfuel.Scribble;

namespace Webfuel.Reporting
{
    internal class ReportScribbleField<TContext> : ReportField where TContext : class
    {
        public ReportScribbleField(string expression)
        {
            FieldType = ReportFieldType.Expression;
            Expression = expression;
            ScribbleScript = ScribbleCompiler.CompileExpression<TContext>(Expression);
        }

        [JsonIgnore]
        public string Expression { get; }

        [JsonIgnore]
        public ScribbleExpression<TContext> ScribbleScript { get; }

        public override Task<object?> Evaluate(object context, ReportBuilder builder)
        {
            return ScribbleScript.EvaluateAsync((TContext)context);
        }
    }
}
