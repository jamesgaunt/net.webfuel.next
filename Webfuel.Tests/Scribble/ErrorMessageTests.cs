using Microsoft.Extensions.DependencyInjection;
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
    public class ErrorMessageTests
    {
        [Fact]
        public async Task Tests()
        {
            // Parser errors (includes position in the source)

            AssertError("var x =", "[7, 1] Expected primary expression");

            AssertError(@"
foreach(var item in Args.GetItems() {
}
", "[35, 2] Expected foreach loop closing parenthesis");

            AssertError("StringProperty.NoSuchMethod();", "No suitable method 'NoSuchMethod' found on type 'String'");

            AssertError("ReadonlyProperty = \"XXX\";", "Property 'ReadonlyProperty' does not have a public setter on type 'Environment'");

            // Missing Properties
           AssertError("MissingProperty = 6;", "Property 'MissingProperty' does not exist on type 'Environment'");
           AssertError("TestApi.NoSuchProperty = 6;", "Property 'NoSuchProperty' does not exist on type 'TestApi'");

            // Assigning Invalid Type
           AssertError("TestApi.ApiPropertyA = 6;", "Cannot assign value of type 'Int32' to 'TestApi.ApiPropertyA'");
           AssertError("TestApi.ApiPropertyA = \"BAR\";", "NO_ERROR");

            // Get Only Properties
           AssertError("TestApi.GetOnlyProperty = 6;", "Property 'GetOnlyProperty' does not have a public setter on type 'TestApi'");
           AssertError("var x = TestApi.GetOnlyProperty;", "NO_ERROR");

            // Set Only Properties
           AssertError("TestApi.SetOnlyProperty = 6;", "NO_ERROR");
           AssertError("var x = TestApi.SetOnlyProperty;", "Property 'SetOnlyProperty' does not have a public getter on type 'TestApi'");

            // Assigning To A Method
           AssertError("ApiMethodA() = 4;", "Expression 'ApiMethodA()' cannot be assigned to");

            // Lambda type deduction
            //AssertError("return StringList.Count((s) => 99);", "Lamda returns 'Int32' but expected 'Boolean'");
            AssertError("return StringList.Count((s) => StringProperty.XXX);", "Property 'XXX' does not exist on type 'String'");

            // Index assignment to read only index
            AssertError("ReadOnlyDictionary[\"XXX\"] = \"XXX\";", "Cannot assign to readonly index");

            // Initialiser Type Mismatch
            AssertError("new TestConstructable { DoesntExist = true };", "Property 'DoesntExist' does not exist on type 'TestConstructable'");
            AssertError("new TestConstructable { Value = \"x\" };", "Cannot assign value of type 'String' to 'System.Int32' when initialising 'Value'");

            // ScribbleValidator
            await AssertErrorAsync("ValidatorTest(42);", "NO_ERROR");
            await AssertErrorAsync("ValidatorTest(99);", "We don't like 99");
        }

        [Fact]
        public async Task TestFocus()
        {
            await AssertErrorAsync("ValidatorTest(42);", "NO_ERROR");
            await AssertErrorAsync("ValidatorTest(99);", "We don't like 99");
        }

        private void AssertError(string source, string errorMessage)
        {
            try
            { 
                var script = ScribbleCompiler.CompileScript<Environment>(source);
            }
            catch(Exception ex)
            {
                Assert.Equal(errorMessage, ex.Message);
                return;
            }
            Assert.Equal("NO_ERROR", errorMessage);
        }

        private async Task AssertErrorAsync(string source, string errorMessage)
        {
            try
            {
                var script = ScribbleCompiler.CompileScript<Environment>(source);

                // Execute Async Validators
                await script.ValidateAsync(validationContext: null);
            }
            catch (Exception ex)
            {
                Assert.Equal(errorMessage, ex.Message);
                return;
            }
            Assert.Equal("NO_ERROR", errorMessage);
        }

        class Environment 
        {
            public String StringProperty { get; set; } = "FOO";

            public String ReadonlyProperty { get; } = "BAR";

            public TestApi TestApi { get { return new TestApi(); } }

            public List<string> StringList { get; set; } = new List<string> { "FOO", "BAR", "WOOWOO" };

            public IReadOnlyDictionary<string, string> ReadOnlyDictionary { get; } = new Dictionary<string, string>() { { "Foo", "Bar" } };

            [ScribbleAsyncValidator(typeof(TestValidator))]
            public void ValidatorTest(int value)
            {

            }
        }

        public class TestApi
        {
            public String ApiMethodA() { return "BAR"; }

            public String ApiPropertyA { get; set; } = "FOO";

            public Int32 GetOnlyProperty { get { return 42; } }

            public Int32 SetOnlyProperty { private get; set; }

        }

        public class TestValidator : IScribbleAsyncValidator
        {
            public Task<string?> ValidateAsync(MethodInfo method, Dictionary<string, object?> literalParameters, object? validationContext)
            {
                if(literalParameters.ContainsKey("value"))
                {
                    var value = literalParameters["value"] as Int32?;
                    if(value != null)
                    {
                        return value.Value == 99 ? Task.FromResult<string?>("We don't like 99") : Task.FromResult<string?>("");
                    }
                }

                return Task.FromResult<string?>("");
            }
        }
    }
}
