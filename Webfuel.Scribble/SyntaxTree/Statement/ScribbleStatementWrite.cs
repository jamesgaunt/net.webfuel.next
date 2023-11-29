using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Scribble
{
    public class ScribbleStatementWriteExpr: ScribbleStatement
    {
        private ScribbleStatementWriteExpr(ScribbleExpr expr, bool htmlAttributeEncode)
        {
            Expr = expr;
            HtmlAttributeEncode = htmlAttributeEncode;
        }

        public ScribbleExpr Expr { get; }

        public bool HtmlAttributeEncode { get; }

        public static ScribbleStatementWriteExpr Create(ScribbleExpr expr, bool htmlAttributeEncode)
        {
            return new ScribbleStatementWriteExpr(expr, htmlAttributeEncode);
        }

        public override string ToString()
        {
            return $"[% {Expr} %]";
        }

        internal override void Bind<T>(ScribbleBindingContext<T> context)
        {
            if (Expr.Bind(context) == Reflection.UnknownType)
                throw new InvalidOperationException("Unable to resove type of expression: " + Expr);
        }

        internal override async Task ExecuteAsync<T>(ScribbleExecutionContext<T> context)
        {
            var value = await context.EvaluateExpressionAsync(Expr);

            if (HtmlAttributeEncode && value != null)
                value = System.Web.HttpUtility.HtmlAttributeEncode(value.ToString());

            context.Writer.Write(value ?? String.Empty);
        }
    }

    public class ScribbleStatementWriteText : ScribbleStatement
    {
        private ScribbleStatementWriteText(string text)
        {
            Text = text;
        }

        public string Text { get; }

        public static ScribbleStatementWriteText Create(ReadOnlySpan<char> text)
        {
            return new ScribbleStatementWriteText(text.ToString());
        }

        public override string ToString()
        {
            return Text;
        }

        internal override void Bind<T>(ScribbleBindingContext<T> context)
        {
        }

        internal override Task ExecuteAsync<T>(ScribbleExecutionContext<T> context)
        {
            context.Writer.Write(Text ?? String.Empty);
            return Task.FromResult<object?>(null);
        }
    }
}
