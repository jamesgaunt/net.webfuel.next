using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webfuel.Scribble;
using Xunit;

namespace Webfuel.Tests.Scribble
{
    public class ExpressionEvaluationTests
    {
        [Fact]
        public void Tests()
        {
            // Numeric Literals
            AssertExpression("3", 3);
            AssertExpression("3.4", 3.4);
            AssertExpression("2.5f", 2.5f);
            AssertExpression("2.3d", 2.3d);
            AssertExpression("4.6m", 4.6m);
            AssertExpression("7L", 7L);

            // Char Literals
            AssertExpression("'c'", 'c');

            // String Literals
            AssertExpression("\"s\"", "s");

            // Boolean Literals
            AssertExpression("true", true);
            AssertExpression("false", false);

            // Null Literals
            AssertExpression("null", null);

            // Numeric Binary Ops
            AssertExpression("4 + 4", 4 + 4);
            AssertExpression("4 - 4f", 4 - 4f);
            AssertExpression("4 * 4d", 4 * 4d);
            AssertExpression("4 + 4m", 4 + 4m);
            AssertExpression("4 / 4", 4 / 4);
            AssertExpression("4 / 4d", 4 / 4d);
            AssertExpression("4 / 4m", 4 / 4m);
            AssertExpression("4f / 4d", 4f / 4d);
            AssertExpression("4f / 4", 4f / 4);
            AssertExpression("4 % 4", 4 % 4);
            AssertExpression("4 % 4d", 4 % 4d);
            AssertExpression("'c' + 4", 'c' + 4);
            AssertExpression("4L * 2L", 4L * 2L);

            // Numeric Comparison
            AssertExpression("8 > 4", 8 > 4);
            AssertExpression("8 >= 4", 8 >= 4);
            AssertExpression("8 < 4", 8 < 4);
            AssertExpression("8 <= 4", 8 <= 4);
            AssertExpression("8 == 4", 8 == 4);
            AssertExpression("8 != 4", 8 != 4);

            // Prefix Ops
            AssertExpression("-3", -3);
            AssertExpression("!true", false);
            AssertExpression("+4.0", +4.0);
            AssertExpression("true || false", true || false);
            AssertExpression("true && false", false && false);
            AssertExpression("\"s\" + 4", "s4");
            AssertExpression("8 % 5", 8 % 5);

            // Conditional
            AssertExpression("true ? 4 : 8", true ? 4 : 8);

            // Index Access
            AssertExpression("\"Webfuel\"[3]", "Webfuel"[3]);
            AssertExpression("Dictionary[\"Foo\"]", "Bar");
            AssertExpression("ReadOnlyDictionary[\"Foo\"]", "Bar");
            AssertExpression("Embed.Dictionary[\"Foo\"]", "Bar");
            AssertExpression("GetIntList()[2]", 4);

            // Index Assignment
            AssertExpression("Embed.Dictionary[\"Foo\"] = \"XXX\"", "XXX");

            // Property Access
            AssertExpression("\"Prefix\".Length", "Prefix".Length);

            // Method Invoke
            AssertExpression("\"Prefix\".Replace('e', 'i')", "Prefix".Replace('e', 'i'));

            // Environment Properties
            AssertExpression("StringProperty", "FOO");
            AssertExpression("StringProperty.Length", 3);
            AssertExpression("Int32Property", 42);

            // Environment Methods
            AssertExpression("Int32Method()", 99);

            // Default Interface Implementaton Methods
            AssertExpression("DefaultImpl()", 180);

            // Automatically Await Async Methods
            AssertExpression("MethodAsync()", 999);

            // Standard Pipes
            AssertExpression("\"FOO\" | ToLower", "foo");

            // Numeric Binary Operations, Nullable Lift
            AssertExpression("NullableInt32Property + Int32Property", 50);
            AssertExpression("Int32Property + NullableInt32Property", 50);
            AssertExpression("NullableInt32Property + NullableInt32Property", 16, typeof(Nullable<Int32>));

            // Coalesce
            AssertExpression("StringProperty ?? StringProperty", "FOO");
            AssertExpression("NullStringProperty ?? StringProperty", "FOO");
            AssertExpression("Int32Property99 ?? Int32Property", 99); // Not C# Stardard - as both not nullable?
            AssertExpression("NullableInt32Property ?? Int32Property", 8);
            AssertExpression("NullInt32Property ?? Int32Property", 42);
            AssertExpression("NullInt32Property ?? NullInt32Property", null, typeof(Nullable<Int32>));
            AssertExpression("EmptyString ?? StringProperty", "");

            // String Coalesce
            AssertExpression("EmptyString $? StringProperty", "FOO");

            // Named Test
            AssertExpression("MethodBindingTest(x: 3)", 42);
            AssertExpression("MethodBindingTest(value: 6, x: 3)", 6);

            // Constructables
            AssertExpression("new TestConstructable().Value", 42);
            AssertExpression("new TestConstructable(99).Value", 99);

            // Generics!
            AssertExpression("new List<String>().Count", 0);
            AssertExpression("TypeName<String>()", "String");
            AssertExpression("GenA()", 0);
            AssertExpression("GenA<String>()", 1);
            AssertExpression("GenA<String, Int32>()", 2);

            // Decimal Implicit Conversion
            AssertExpression("DecimalProperty = 42", 42M);
        }

        [Fact] 
        public void TestFocus()
        {
            AssertExpression("GenA<String>()", 1);
        }

        private void AssertExpression(string source, object? expectedValue, Type? type = null)
        {
            var expression = ScribbleCompiler.CompileExpression<Environment>(source);
            var actualValue = expression.EvaluateAsync(new Environment()).Result;

            Assert.Equal(expectedValue, actualValue);
        }

        public class Environment : ScribbleEnvironment, IDefaultImpl
        {
            public String StringProperty { get; set; } = "FOO";

            public Int32 Int32Property { get; set; } = 42;

            public Int32 Int32Property99 { get; set; } = 99;

            public Decimal DecimalProperty { get; set; } = 12M;

            public Int32? NullableInt32Property { get; set; } = 8;

            public Int32? NullInt32Property { get; set; } = null;

            public String? NullStringProperty { get; set; } = null;

            public String EmptyString { get; set; } = System.String.Empty;

            public List<int> GetIntList() { return new List<int> { 0, 2, 4, 8 }; }

            public string TypeName<T>()
            {
                return typeof(T).Name;
            }

            public int GenA()
            {
                return 0;
            }

            public int GenA<T>()
            {
                return 1;
            }

            public int GenA<U, V>()
            {
                return 2;
            }

            public Int32 Int32Method()
            {
                return 99;
            }

            public Task<Int32> MethodAsync()
            {
                return Task.FromResult(999);
            }

            public Int32 MethodBindingTest(int x, int value = 42, char c = 'c')
            {
                return value;
            }

            public Dictionary<string, string> Dictionary { get; } = new Dictionary<string, string>() { { "Foo", "Bar" } };

            public IReadOnlyDictionary<string, string> ReadOnlyDictionary { get; } = new Dictionary<string, string>() { { "Foo", "Bar" } };

            public EmbeddedType Embed { get; } = new EmbeddedType();
        }

        public interface IDefaultImpl
        {
            int DefaultImpl()
            {
                return 180;
            }
        }

        public class CollectionItem
        {
            public string Name { get; set; } = String.Empty;

            public string Value { get; set; } = String.Empty;
        }

        public class EmbeddedType
        {
            public Dictionary<string, string> Dictionary { get; } = new Dictionary<string, string>() { { "Foo", "Bar" } };
        }
    }
}