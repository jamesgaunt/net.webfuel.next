using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webfuel.Scribble;
using Xunit;
namespace Webfuel.Tests.Scribble
{
    public class TemplateRenderTests
    {
        [Fact]
        public void Tests()
        {
            AssertTemplate("", "");
            AssertTemplate("4;", "4;");
            AssertTemplate("FOO[% Int32Property %]BAR", "FOO42BAR");

            // if
            AssertTemplate("FOO{% if(false) { %}XXX{% } %}BAR", "FOOBAR");
            AssertTemplate("FOO{% if(true) { %}XXX{% } %}BAR", "FOOXXXBAR");

            // foreach
            AssertTemplate("{% foreach(var c in StringProperty) { %}X{% } %}", "XXX");
            AssertTemplate("{% foreach(var c in StringProperty) { %}X[% c %]{% } %}", "XFXOXO");

            // for
            AssertTemplate("{% for(var i = 0; i < 5; i = i + 1) { %}[% i %]{% } %}", "01234");

            // AdditionAssignment Operator
            AssertTemplate("{% for(var i = 0; i < 5; i += 1) { %}[% i %]{% } %}", "01234");

            // PostIncrement Operator
            AssertTemplate("{% for(var i = 0; i < 5; i++) { %}[% i %]{% } %}", "01234");

            // Trimming
            AssertTemplate("  {%- for(var i = 0; i < 5; i++) { -%}  [% i %]  {%- } -%}  ", "01234");
            AssertTemplate("  {%- for(var i = 0; i < 5; i++) { %}  [%- i -%]  {% } -%}  ", "01234");
            AssertTemplate("  {%- for(var i = 0; i < 5; i++) { -%}  [%- i -%]  {%- } -%}  ", "01234");
            AssertTemplate("  {% for(var i = 0; i < 5; i++) { -%}  [%- i -%]  {%- } %}  ", "  01234  ");

            // Html Attribute
            AssertTemplate("<div class='[%< \"<\" >%]'>", "<div class='&lt;'>");
        }

        private void AssertTemplate(string source, object expectedValue)
        {
            var template = ScribbleCompiler.CompileTemplate<Environment>(source);
            var actualValue = template.RenderAsync(new Environment()).Result;

            Assert.Equal(expectedValue, actualValue);
        }

        public class Environment
        {
            public String StringProperty { get; set; } = "FOO";

            public Int32 Int32Property { get; set; } = 42;

            public void ModifyInt32(int value)
            {
                Int32Property += value;
            }
        }
    }
}
