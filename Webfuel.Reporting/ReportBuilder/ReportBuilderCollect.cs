using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    public static class ReportBuilderCollect
    {
        public static object? Collect(ReportColumn column, List<object> values)
        {
            if(values.Count == 0)
                return null;

            if(values.Count == 1 && column.Collection == ReportColumnCollection.Default)
                return values[0];

            switch (column.Collection)
            {
                case ReportColumnCollection.List:
                    return List(values);
                case ReportColumnCollection.Sum:
                    return Sum(values);
                case ReportColumnCollection.Count:
                    return Count(values);
                case ReportColumnCollection.Avg:
                    return Average(values);
                case ReportColumnCollection.Min:
                    return Min(values);
                case ReportColumnCollection.Max:
                    return Max(values);
                case ReportColumnCollection.ListDistinct:
                    return ListDistinct(values);
            }

            if (column.FieldType == ReportFieldType.Number)
                return Sum(values);

            return List(values);
        }

        public static object? List(List<object> values)
        {
            return String.Join(", ", values);
        }

        public static object? Sum(List<object> values)
        {
            var sum = 0D;
            foreach (var value in values)
            {
                if (IsNumericType(value.GetType()))
                    sum += Convert.ToDouble(value);
                else
                    throw new InvalidOperationException("Collection contains a non-numeric value");
            }
            return sum;
        }

        public static object? Count(List<object> values)
        {
            return values.Count;
        }

        public static object? Average(List<object> values)
        {
            var sum = 0D;
            var count = 0;
            foreach (var value in values)
            {
                if (IsNumericType(value.GetType()))
                {
                    sum += Convert.ToDouble(value);
                    count++;
                }
                else
                    throw new InvalidOperationException("Collection contains a non-numeric value");
            }
            return count == 0 ? 0 : sum / count;
        }

        public static object? Min(List<object> values)
        {
            var min = double.MaxValue;
            foreach (var value in values)
            {
                if (IsNumericType(value.GetType()))
                    min = Math.Min(min, Convert.ToDouble(value));
                else
                    throw new InvalidOperationException("Collection contains a non-numeric value");
            }
            return min;
        }

        public static object? Max(List<object> values)
        {
            var max = double.MinValue;
            foreach (var value in values)
            {
                if (IsNumericType(value.GetType()))
                    max = Math.Max(max, Convert.ToDouble(value));
                else
                    throw new InvalidOperationException("Collection contains a non-numeric value");
            }
            return max;
        }

        public static object? ListDistinct(List<object> values)
        {
            return String.Join(", ", values.Distinct());
        }

        static bool IsNumericType(Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }

    }
}
