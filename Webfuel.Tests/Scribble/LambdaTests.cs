using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webfuel.Scribble;
using Xunit;
namespace Webfuel.Tests.Scribble
{
    public class LambdaTests
    {
        [Fact]
        public void Tests()
        {
            AssertTest("return StringList.Count((s) => s.Length == 3);", 2);
            AssertTest("return StringList.Where((s) => s == \"WOOWOO\").Count();", 1);
            AssertTest("return Models.Where((p) => p.Name == \"FOO\").Count();", 3);
            AssertTest("return Models.Map((p) => p.Name)[0];", "FOO");
            AssertTest("return Models.Sum((p) => p.Age);", 15);
        }

        [Fact]
        public void TestFocus()
        {
            AssertTest("return Models.Map((p) => p.Name)[0];", "FOO");
        }

        private void AssertTest(string source, object expectedValue)
        {
            var script = ScribbleCompiler.CompileScript<Environment>(source);
            var actualValue = script.ExecuteAsync(new Environment()).Result;

            Assert.Equal(expectedValue, actualValue);
        }

        public class Environment
        {
            public List<string> StringList { get; set; } = new List<string> { "FOO", "BAR", "WOOWOO" };

            public List<Model> Models { get; } = new List<Model>
            {
                new Model { Name = "FOO", Age = 1 },
                new Model { Name = "FOO", Age = 2 },
                new Model { Name = "FOO", Age = 3 },
                new Model { Name = "BAR", Age = 4 },
                new Model { Name = "BAR", Age = 5 },
            };
        }

        public class Model
        {
            public string Name { get; set; } = String.Empty;

            public int Age { get; set; }
        }
    }
}
