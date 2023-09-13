using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Webfuel
{
    public partial class QueryParser
    {
        private IQueryLexer Lexer;

        QueryParser(IQueryLexer lexer)
        {
            Lexer = lexer;
        }

        // Error Handling

        int ErrorIndex { get; set; } = 0;

        string ErrorMessage { get; set; } = String.Empty;

        void PushError(string message, int index)
        {
            if (index > ErrorIndex)
            {
                ErrorIndex = index;
                ErrorMessage = message;
            }
        }

        public static QueryFilter ParseFilter(string source)
        {
            using (var lexer = new QueryLexer(source))
            {
                var index = lexer.Tokenise();
                if (index != 0)
                    throw new InvalidOperationException("Unrecognised tokens in query");

                var parser = new QueryParser(lexer);
                var filter = parser.ParseFilter();

                if(filter == null)
                {
                    if (parser.ErrorIndex > 0)
                        throw new InvalidOperationException(parser.ErrorMessage);
                    throw new InvalidOperationException("Unrecognised parser error in query");
                }
                return filter;
            }
        }
    }
}
