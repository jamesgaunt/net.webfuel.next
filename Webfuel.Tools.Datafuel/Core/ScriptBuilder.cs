using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Webfuel.Tools.Datafuel
{
    public class ScriptBuilder
    {
        private readonly StringBuilder Writer = new StringBuilder();

        public ScriptBuilder Clear()
        {
            Writer.Clear();
            return this;
        }

        public ScriptBuilder Write(string text)
        {
            Writer.Append(text);
            return this;
        }

        public ScriptBuilder WriteLine()
        {
            WriteLine(String.Empty);
            return this;
        }

        public ScriptBuilder WriteLine(string text)
        {
            Writer.Append(text).Append("\n");
            return this;
        }

        public ScriptBuilderBrace OpenBrace(string text, bool trailingSemicolon = false)
        {
            WriteLine(text);
            WriteLine("{");
            return new ScriptBuilderBrace(this, trailingSemicolon);
        }

        public override string ToString()
        {
            var output = new StringBuilder();
            var lines = Writer.ToString().Split('\n');
            var indent = 0;
            var inCase = false;
            foreach (var line in lines)
            {
                var trim = line.Trim();

                if (trim.StartsWith("}") || trim.StartsWith("]"))
                {
                    indent--;
                    if (inCase)
                    {
                        indent--;
                        inCase = false;
                    }
                }

                if (trim.StartsWith("case ") && inCase)
                    indent--;

                if (trim.StartsWith("."))
                    indent++;

                for (int i = 0; i < indent * 4; i++)
                    output.Append(" ");

                output.Append(trim);
                output.Append("\n");

                if (trim.StartsWith("case "))
                {
                    indent++;
                    inCase = true;
                }

                if (trim.StartsWith("}") || trim.StartsWith("]"))
                    indent++;

                if (trim.StartsWith("."))
                    indent--;

                // Determine the impact on indent
                indent += line.Count(p => p == '{') - line.Count(p => p == '}') + line.Count(p => p == '[') - line.Count(p => p == ']');
            }
            return output.ToString();
        }

        public class ScriptBuilderBrace : IDisposable
        {
            private readonly ScriptBuilder ScriptBuilder;

            public ScriptBuilderBrace(ScriptBuilder scriptBuilder, bool trailingSemicolon)
            {
                ScriptBuilder = scriptBuilder;
                TrailingSemicolon = trailingSemicolon;
            }

            bool TrailingSemicolon;

            public void Dispose()
            {
                ScriptBuilder.WriteLine($"}}{(TrailingSemicolon ? ";" : "")}");
            }
        }
    }
}
