using System;
using System.Collections.Generic;
using System.Text;

namespace Webfuel.Scribble
{
    static class ScribbleBinaryOpBinder
    {
        public static object? EvaluateBinaryOp(object? lhs, ScribbleBinaryOp op, object? rhs)
        {
            switch (op)
            {
                case ScribbleBinaryOp.Add:
                    return (dynamic)lhs! + (dynamic)rhs!;

                case ScribbleBinaryOp.Subtract:
                    return (dynamic)lhs! - (dynamic)rhs!;

                case ScribbleBinaryOp.Multiply:
                    return (dynamic)lhs! * (dynamic)rhs!;

                case ScribbleBinaryOp.Divide:
                    return (dynamic)lhs! / (dynamic)rhs!;

                case ScribbleBinaryOp.Mod:
                    return (dynamic)lhs! % (dynamic)rhs!;

                case ScribbleBinaryOp.LessThan:
                    return (dynamic)lhs! < (dynamic)rhs!;

                case ScribbleBinaryOp.GreaterThan:
                    return (dynamic)lhs! > (dynamic)rhs!;

                case ScribbleBinaryOp.LessThanOrEqual:
                    return (dynamic)lhs! <= (dynamic)rhs!;

                case ScribbleBinaryOp.GreaterThanOrEqual:
                    return (dynamic)lhs! >= (dynamic)rhs!;

                case ScribbleBinaryOp.Equal:
                    return (dynamic)lhs! == (dynamic)rhs!;

                case ScribbleBinaryOp.NotEqual:
                    return (dynamic)lhs! != (dynamic)rhs!;

                case ScribbleBinaryOp.LogicalOr:
                    return (dynamic)lhs! || (dynamic)rhs!;

                case ScribbleBinaryOp.LogicalAnd:
                    return (dynamic)lhs! && (dynamic)rhs!;

                case ScribbleBinaryOp.Coalesce:
                    return (dynamic)lhs! ?? (dynamic)rhs!;

                case ScribbleBinaryOp.StringCoalesce:
                    return String.IsNullOrWhiteSpace(lhs?.ToString()) ? rhs : lhs;
            }
            throw new InvalidOperationException("Unsupported binary operation");
        }

        public static Type ResolveBinaryOp(Type lhs, ScribbleBinaryOp op, Type rhs)
        {
            // Unwind Nullable wrappers

            var nullables = 0;
            if (Reflection.IsNullable(lhs))
            {
                lhs = Reflection.NullableUnderlyingType(lhs);
                nullables++;
            }
            if (Reflection.IsNullable(rhs))
            {
                rhs = Reflection.NullableUnderlyingType(rhs);
                nullables++;
            }

            if (op == ScribbleBinaryOp.LessThan || op == ScribbleBinaryOp.GreaterThan || op == ScribbleBinaryOp.LessThanOrEqual || op == ScribbleBinaryOp.GreaterThanOrEqual || op == ScribbleBinaryOp.Equal || op == ScribbleBinaryOp.NotEqual)
                return typeof(bool);

            if (op == ScribbleBinaryOp.LogicalAnd || op == ScribbleBinaryOp.LogicalOr)
            {
                if (Reflection.IsBoolean(lhs) && Reflection.IsBoolean(rhs))
                    return lhs;
                return Reflection.UnknownType;
            }

            if(op == ScribbleBinaryOp.Coalesce || op == ScribbleBinaryOp.StringCoalesce)
            {
                var result = Reflection.UnknownType;
                if (Reflection.IsNumeric(lhs) && Reflection.IsNumeric(rhs))
                {
                    if (Reflection.HasImplicitNumericConversion(from: lhs, to: rhs))
                        result = rhs;
                    else if (Reflection.HasImplicitNumericConversion(from: rhs, to: lhs))
                        result = lhs;
                    if (nullables == 2)
                        return typeof(Nullable<>).MakeGenericType(result); // If both arguments are nullable result is nullable
                    return result;
                }
                else if(lhs == rhs)
                {
                    result = lhs;
                    if (nullables == 2)
                        return typeof(Nullable<>).MakeGenericType(result); // If both arguments are nullable result is nullable
                    return result;

                }
                return Reflection.UnknownType;
            }

            if (Reflection.IsNumeric(lhs) && Reflection.IsNumeric(rhs))
            {
                var result = ResolveNumericBinaryOp(lhs, op, rhs);
                if(nullables == 2)
                    return typeof(Nullable<>).MakeGenericType(result); // If both arguments are nullable result is nullable
                return result;
            }

            if (Reflection.IsString(lhs) || Reflection.IsString(rhs))
                return ResolveStringBinaryOp(lhs, op, rhs);

            return Reflection.UnknownType;
        }

        // Implementation

        static Type ResolveNumericBinaryOp(Type lhs, ScribbleBinaryOp op, Type rhs)
        {
            if (op == ScribbleBinaryOp.Add || op == ScribbleBinaryOp.Subtract || op == ScribbleBinaryOp.Multiply || op == ScribbleBinaryOp.Divide || op == ScribbleBinaryOp.Mod)
            {
                if (Reflection.HasImplicitNumericConversion(from: lhs, to: rhs))
                    return rhs;
                if (Reflection.HasImplicitNumericConversion(from: rhs, to: lhs))
                    return lhs;
                return Reflection.UnknownType;
            }

            return Reflection.UnknownType;
        }

        // At least one type is String

        static Type ResolveStringBinaryOp(Type lhs, ScribbleBinaryOp op, Type rhs)
        {
            if (op == ScribbleBinaryOp.Add)
                return typeof(String);

            // We can only add strings, no other operations make sense
            return Reflection.UnknownType;
        }
    }
}
