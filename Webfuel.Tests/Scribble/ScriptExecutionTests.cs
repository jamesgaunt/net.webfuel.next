using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Webfuel.Scribble;
using Xunit;
namespace Webfuel.Tests.Scribble
{
    public class ScriptExecutionTests
    {
        [Fact]
        public void Tests()
        {
            AssertScript("", "");
            AssertScript("4;", "");
            AssertScript("return;", null);
            AssertScript("return 4;", 4);

            AssertScript("return Int32Property;", 42);
            AssertScript("return Int32Property + 2;", 44);

            AssertScript(@"
if(4 > 3)
    return true;
return false;
", true);

            AssertScript(@"
ModifyInt32(4);
return Int32Property;
", 46);

            AssertScript(@"
Int32Property = 4;
return Int32Property;
", 4);

            AssertScript(@"
Int32Property = 0;
foreach(var c in ""FOOBAR"") {
    Int32Property = Int32Property + 1;
}
return Int32Property;
", 6);

            AssertScript(@"
var list = new List<String>();
list.Add(""FOO"");
list.Add(""BAR"");
return list[1];
", "BAR");

            AssertScript(@"
var test = new TestConstructable {
    Value = 99
};
return test.Value;
", 99);

            // Test logical OR/AND short circuit

            AssertScript(@"
var result = TrueCounter || FalseCounter;
return Counter;
", 1);

            AssertScript(@"
var result = FalseCounter || TrueCounter;
return Counter;
", 2);

            AssertScript(@"
var result = TrueCounter && FalseCounter;
return Counter;
", 2);

            AssertScript(@"
var result = FalseCounter && TrueCounter;
return Counter;
", 1);

        }

        private void AssertScript(string source, object? expectedValue)
        {
            var script = ScribbleCompiler.CompileScript<Environment>(source);
            var actualValue = script.ExecuteAsync(new Environment()).Result;

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


            public int Counter { get; set; }

            public bool TrueCounter { get { Counter++; return true; } }

            public bool FalseCounter { get { Counter++; return false; } }

        }

    }

}
