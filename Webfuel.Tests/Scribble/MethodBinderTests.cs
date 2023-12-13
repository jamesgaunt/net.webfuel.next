using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webfuel.Scribble;
using Xunit;

namespace Webfuel.Tests.Scribble
{
    public class MethodBinderTests
    {
        [Fact]
        public void Tests()
        {
            AssertTest("MethodA()", 1);

            AssertTest("MethodA(3)", 2);
            AssertTest("MethodA(x:3)", 2);

            AssertTest("MethodA(1, 2)", 3);
            AssertTest("MethodA(x: 1, y: 2)", 3);
            AssertTest("MethodA(y: 1, x: 2)", 3);
            AssertTest("MethodA(1, y: 2)", 3);

            AssertTest("MethodA(\"\")", 4);

            AssertTest("MethodB(1)", 3);
            AssertTest("MethodB(1, 4)", 4);

            AssertTest("MethodC(1)", 1);
            AssertTest("MethodC(1, 4)", 2);
            AssertTest("MethodC(z: 1)", 3);

            // Extension Methods
            AssertTest("\"xxx\".WrapWithTag(\"div\")", "<div>xxx</div>");
            AssertTest("\"xxx\" | WrapWithTag: \"div\"", "<div>xxx</div>");
            AssertTest("\"xxx\".Contains('x')", true);
            AssertTest("\"xxx\".Contains('y')", false);

            // Generic Extension Methods
            AssertTest("StringList.Count()", 3);
            AssertTest("StringList.Skip(1).Count()", 2);

            // Check
            AssertTest("TemplateApi.Render(\"A\")", 0);
            AssertTest("TemplateApi.Render(\"A\", Args)", 1);

            // Unwrap Nullable Arguments
            AssertTest("GuidTest(TestGuid)", 42);
            AssertTest("GuidTest(TestNullableGuid)", 42);

            // Generic Binding
            AssertTest("TypeName(TestGuid)", "Guid");
        }

        [Fact]
        public void TestFocus()
        {
            AssertTest("TypeName(TestGuid)", "Guid");
        }

        private void AssertTest(string source, object expectedValue)
        {
            var script = ScribbleCompiler.CompileExpression<Environment>(source);
            var actualValue = script.EvaluateAsync(new Environment()).Result;

            Assert.Equal(expectedValue, actualValue);
        }

        public class Environment
        {
            public int MethodA() { return 1; }
            public int MethodA(int x = 3) { return 2; }
            public int MethodA(int x = 3, int y = 4) { return 3; }
            public int MethodA(string s) { return 4; }

            public int MethodB(int x, int y = 3) { return y; }

            public int MethodC(int x) { return 1; }
            public int MethodC(int x, int y = 3) { return 2; }
            public int MethodC(int x = 1, int y = 3, int z = 4) { return 3; }

            public List<string> StringList { get; set; } = new List<string> { "FOO", "BAR", "WOOWOO" };

            public ITemplateApi TemplateApi { get; set; } = new TemplateApi();

            public TemplateArgs Args { get; set; } = new TemplateArgs();


            public int GuidTest(Guid id) { return 42; }
            public Guid TestGuid { get; } = Guid.Empty;

            public Guid? TestNullableGuid { get; } = Guid.Empty;


            public string TypeName<T>(T value) { return typeof(T).Name; }

        }
        public class TemplateArgs
        {
        }

        public interface ITemplateApi
        {


            int Render(string uniqueId);

            int Render(string uniqueId, TemplateArgs args);
        }

        public class TemplateApi : ITemplateApi
        {
            public int Render(string uniqueId)
            {
                return 0;
            }

            public int Render(string uniqueId, TemplateArgs args)
            {
                return 1;
            }
        }
    }
}
