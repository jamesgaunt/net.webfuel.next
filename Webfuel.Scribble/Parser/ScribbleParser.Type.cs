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
        ScribbleTypeName? ParseTypeName(int index, ref int consumed)
        {
            int start = index, temp = 0;

            if(!Lexer.Is(index, ScribbleTokenType.IdentifierToken))
            {
                PushError("Expected type name", index);
                return null;
            }

            // Name
            var name = Lexer.Extract(index);
            index++;

            // Arguments
            var arguments = TypeArgumentList(index, ref temp);
            index += temp;

            consumed = index - start;
            return ScribbleTypeName.Create(name.ToString(), arguments);
        }
    }
}
