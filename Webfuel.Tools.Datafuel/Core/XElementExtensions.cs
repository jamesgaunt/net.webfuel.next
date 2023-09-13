using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Webfuel
{
    public static class XElementExtensions
    {
        public static string DeepName(this XElement element)
        {
            var sb = new StringBuilder();
            while (element != null) {
                if (sb.Length > 0)
                    sb.Insert(0, ".");
                sb.Insert(0, element.Name);
                
                if (element.Parent == null)
                    break;
                element = element.Parent;
            }
            return sb.ToString();
        }

        public static String? StringProperty(this XElement element, string name)
        {
            XAttribute? attribute = element.Attribute(name);
            if (attribute == null)
                return null;
            return attribute.Value;
        }

        public static Boolean? BooleanProperty(this XElement element, string name)
        {
            var text = StringProperty(element, name);
            if (text == null)
                return null;
            return bool.Parse(text);
        }

        public static Int32? Int32Property(this XElement element, string name)
        {
            var text = StringProperty(element, name);
            if (text == null)
                return null;
            return Int32.Parse(text);
        }

        public static Int64? Int64Property(this XElement element, string name)
        {
            var text = StringProperty(element, name);
            if (text == null)
                return null;
            return Int64.Parse(text);
        }

        public static Decimal? DecimalProperty(this XElement element, string name)
        {
            var text = StringProperty(element, name);
            if (text == null)
                return null;
            return decimal.Parse(text);
        }

        public static Double? DoubleProperty(this XElement element, string name)
        {
            var text = StringProperty(element, name);
            if (text == null)
                return null;
            return double.Parse(text);
        }

        public static T? EnumProperty<T>(this XElement element, string name) where T: struct
        {
            var value = element.StringProperty(name);
            if (value == null)
                return null;
            T result;
            if (!Enum.TryParse<T>(value, true, out result))
                throw new InvalidOperationException("Unable to parse enum: " + value);
            return result;
        }
    }
}
