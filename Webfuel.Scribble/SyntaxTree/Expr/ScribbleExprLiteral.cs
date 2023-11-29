using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Webfuel.Scribble
{
    public interface IScribbleExprLiteral
    {
        object? GetValue();
    }

    public abstract class ScribbleExprLiteral<TLiteral> : ScribbleExpr, IScribbleExprLiteral
    {
        protected ScribbleExprLiteral(TLiteral value)
        {
            Value = value;
        }

        public TLiteral Value { get; protected set; }

        internal override Type Bind<T>(ScribbleBindingContext<T> context)
        {
            return typeof(TLiteral);
        }

        internal override IEnumerable<ScribbleExpr> GetPrerequisites<T>(ScribbleExecutionContext<T> context)
        {
            yield break;
        }

        internal override object? Evaluate<T>(ScribbleExecutionContext<T> context, List<object?> prerequisites)
        {
            return Value;
        }

        public object? GetValue()
        {
            return Value;
        }
    }

    public class ScribbleExprLiteralString : ScribbleExprLiteral<String>
    {
        private ScribbleExprLiteralString(string value): base(value)
        {
        }

        public static ScribbleExprLiteralString Create(string value)
        {
            return new ScribbleExprLiteralString(value);
        }

        public override string ToString()
        {
            return $"\"{StringUtility.EscapeString(Value)}\"";
        }
    }

    public class ScribbleExprLiteralChar : ScribbleExprLiteral<char>
    {
        private ScribbleExprLiteralChar(char value): base(value)
        {
        }

        public override string ToString()
        {
            return $"\'{StringUtility.EscapeString(Value.ToString())}\'";
        }

        // Pool

        static ConcurrentDictionary<char, ScribbleExprLiteralChar> Pool = new ConcurrentDictionary<char, ScribbleExprLiteralChar>();

        public static ScribbleExprLiteralChar Create(char value)
        {
            return Pool.GetOrAdd(value, (_value) => new ScribbleExprLiteralChar(_value));
        }
    }

    public class ScribbleExprLiteralBoolean : ScribbleExprLiteral<bool>
    {
        private ScribbleExprLiteralBoolean(bool value): base(value)
        {
        }

        public override string ToString()
        {
            return Value.ToString().ToLower();
        }

        // Pool

        static ConcurrentDictionary<bool, ScribbleExprLiteralBoolean> Pool = new ConcurrentDictionary<bool, ScribbleExprLiteralBoolean>();

        public static ScribbleExprLiteralBoolean Create(bool value)
        {
            return Pool.GetOrAdd(value, (_value) => new ScribbleExprLiteralBoolean(_value));
        }
    }

    public class ScribbleExprLiteralNull : ScribbleExprLiteral<object?>
    {
        private ScribbleExprLiteralNull(): base(null)
        {
        }

        public override string ToString()
        {
            return "null";
        }

        // Pool

        public static ScribbleExprLiteralNull Create()
        {
            return NULL;
        }

        static ScribbleExprLiteralNull NULL = new ScribbleExprLiteralNull();
    }

    public class ScribbleExprLiteralInt32 : ScribbleExprLiteral<Int32>
    {
        private ScribbleExprLiteralInt32(Int32 value): base(value)
        {
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        // Pool

        static ConcurrentDictionary<Int32, ScribbleExprLiteralInt32> Pool = new ConcurrentDictionary<Int32, ScribbleExprLiteralInt32>();

        public static ScribbleExprLiteralInt32 Create(Int32 value)
        {
            return Pool.GetOrAdd(value, (_value) => new ScribbleExprLiteralInt32(_value));
        }
    }

    public class ScribbleExprLiteralInt64 : ScribbleExprLiteral<Int64>
    {
        private ScribbleExprLiteralInt64(Int64 value) : base(value)
        {
        }

        public override string ToString()
        {
            return Value.ToString() + "l";
        }

        // Pool

        static ConcurrentDictionary<Int64, ScribbleExprLiteralInt64> Pool = new ConcurrentDictionary<Int64, ScribbleExprLiteralInt64>();

        public static ScribbleExprLiteralInt64 Create(Int64 value)
        {
            return Pool.GetOrAdd(value, (_value) => new ScribbleExprLiteralInt64(_value));
        }
    }

    public class ScribbleExprLiteralDouble : ScribbleExprLiteral<Double>
    {
        private ScribbleExprLiteralDouble(Double value): base(value)
        {
        }

        public static ScribbleExprLiteralDouble Create(Double value)
        {
            return new ScribbleExprLiteralDouble(value);
        }

        public override string ToString()
        {
            return Value.ToString() + "d";
        }
    }
    public class ScribbleExprLiteralSingle : ScribbleExprLiteral<Single>
    {
        private ScribbleExprLiteralSingle(Single value): base(value)
        {
        }

        public static ScribbleExprLiteralSingle Create(Single value)
        {
            return new ScribbleExprLiteralSingle(value);
        }

        public override string ToString()
        {
            return Value.ToString() + "f";
        }
    }

    public class ScribbleExprLiteralDecimal : ScribbleExprLiteral<Decimal>
    {
        private ScribbleExprLiteralDecimal(Decimal value): base(value)
        {
        }

        public static ScribbleExprLiteralDecimal Create(Decimal value)
        {
            return new ScribbleExprLiteralDecimal(value);
        }

        public override string ToString()
        {
            return Value.ToString() + "m";
        }
    }
}
