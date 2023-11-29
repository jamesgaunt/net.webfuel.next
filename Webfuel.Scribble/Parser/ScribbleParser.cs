using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Webfuel.Scribble
{
    internal partial class ScribbleParser
    {
        private ScribbleLexer Lexer;

        protected ScribbleParser(ScribbleLexer lexer)
        {
            Lexer = lexer;
        }

        // Static API

        public static ScribbleExpr ParseExpression(string source)
        {
            using (var lexer = new ScribbleLexer(source))
            {
                var index = lexer.Tokenise(template: false);
                if (index != 0)
                {
                    var pos = lexer.GetSourcePosition(index);
                    throw new InvalidOperationException($"[{pos.Item1}, {pos.Item2}] Unrecognised tokens in source");
                }

                var parser = new ScribbleParser(lexer);
                var expression = parser.ParseExpression();
                if (expression == null)
                {
                    if (parser.ErrorIndex > 0)
                    {
                        var pos = lexer.GetSourcePosition(lexer.At(parser.ErrorIndex - 1)?.Index ?? 0);
                        throw new InvalidOperationException($"[{pos.Item1}, {pos.Item2}] {parser.ErrorMessage}");
                    }

                    throw new InvalidOperationException("Unrecognised parser error");
                }
                return expression;
            }
        }

        public static List<ScribbleStatement> ParseStatements(string source)
        {
            using (var lexer = new ScribbleLexer(source))
            {
                var index = lexer.Tokenise(template: false);
                if (index != 0)
                {
                    var pos = lexer.GetSourcePosition(index);
                    throw new InvalidOperationException($"[{pos.Item1}, {pos.Item2}] Unrecognised tokens in source");
                }

                var parser = new ScribbleParser(lexer);
                var statements = parser.ParseStatements();
                if (statements == null)
                {
                    if (parser.ErrorIndex > 0)
                    {
                        var pos = lexer.GetSourcePosition(lexer.At(parser.ErrorIndex - 1)?.Index ?? 0);
                        throw new InvalidOperationException($"[{pos.Item1}, {pos.Item2}] {parser.ErrorMessage}");
                    }

                    throw new InvalidOperationException("Unrecognised parser error");
                }
                return statements;
            }
        }

        public static List<ScribbleStatement> ParseTemplate(string source)
        {
            using (var lexer = new ScribbleLexer(source))
            {
                var index = lexer.Tokenise(template: true);
                if (index != 0)
                {
                    var pos = lexer.GetSourcePosition(index);
                    throw new InvalidOperationException($"[{pos.Item1}, {pos.Item2}] Unrecognised tokens in source");
                }

                var parser = new ScribbleParser(lexer);
                var statements = parser.ParseStatements();
                if (statements == null)
                {
                    if (parser.ErrorIndex > 0)
                    {
                        var pos = lexer.GetSourcePosition(lexer.At(parser.ErrorIndex - 1)?.Index ?? 0);
                        throw new InvalidOperationException($"[{pos.Item1}, {pos.Item2}] {parser.ErrorMessage}");
                    }

                    throw new InvalidOperationException("Unrecognised parser error");
                }
                return statements;
            }
        }

        // Error Handling

        public int ErrorIndex { get; private set; } = 0;

        public string ErrorMessage { get; private set; } = String.Empty;

        void PushError(string message, int index)
        {
            if (index > ErrorIndex)
            {
                ErrorIndex = index;
                ErrorMessage = message;
            }
        }


    }
}
