using System;
using System.Collections.Generic;
using System.Text;

namespace Webfuel.Tools.Typefuel
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

        public ScriptBuilderBrace OpenBrace(string text)
        {
            WriteLine(text + " {");
            return new ScriptBuilderBrace(this);
        }

        public override string ToString()
        {
            return Writer.ToString();
        }

        public class ScriptBuilderBrace: IDisposable
        {
            private readonly ScriptBuilder ScriptBuilder;

            public ScriptBuilderBrace(ScriptBuilder scriptBuilder)
            {
                ScriptBuilder = scriptBuilder;
            }

            public void Dispose()
            {
                ScriptBuilder.WriteLine("}");
            }
        }
    }
}
