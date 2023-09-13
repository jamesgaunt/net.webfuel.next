using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Webfuel.Tools.Datafuel
{
    public class XSDElement
    {
        public string Name { get; set; } = String.Empty;

        public bool Required { get; set; }

        public bool Singular { get; set; }

        public bool DoNotValidate { get; set; }

        public List<XSDElement> Elements { get; } = new List<XSDElement>();

        public List<XSDAttribute> Attributes { get; } = new List<XSDAttribute>();

        public void Validate(XElement element, string filename)
        {
            if (DoNotValidate)
                return;

            // Elements

            foreach (var childElement in element.Elements())
            {
                var childXSDElement = Elements.FirstOrDefault(p => p.Name == childElement.Name);
                if (childXSDElement == null)
                {
                    Console.Error.WriteLine($"XSD: Element {childElement.DeepName()} should not exist in {filename}");
                    Console.ReadKey();
                }
                else
                {
                    childXSDElement.Validate(childElement, filename);
                }
            }

            foreach (var childXSDElement in Elements)
            {
                var childElement = element.Elements(childXSDElement.Name);
                if (childXSDElement.Required && childElement.Count() == 0)
                {
                    Console.Error.WriteLine($"XSD: Element {childXSDElement.Name} is required on {Name} in {filename}");
                    Console.ReadKey();
                }

                if (childXSDElement.Singular && childElement.Count() > 1)
                {
                    Console.Error.WriteLine($"XSD: Element {childXSDElement.Name} is singular on {Name} in {filename}");
                    Console.ReadKey();
                }
            }

            // Attributes

            foreach (var childAttribute in element.Attributes())
            {
                var childXSDAttribute = Attributes.FirstOrDefault(p => p.Name == childAttribute.Name);
                if (childXSDAttribute == null)
                {
                    Console.Error.WriteLine($"XSD: Attribute {childAttribute.Parent!.DeepName()}.{childAttribute.Name} should not exist in {filename}");
                    Console.ReadKey();
                }
                else if (childXSDAttribute.Values.Count > 0 && !childXSDAttribute.Values.Contains(childAttribute.Value))
                {
                    Console.Error.WriteLine($"XSD: Attribute {childAttribute.Parent!.DeepName()}.{childAttribute.Name} has an invalid value in {filename}");
                    Console.ReadKey();
                }
            }

            foreach (var childXSDAttribute in Attributes)
            {
                var childAttribute = element.Attribute(childXSDAttribute.Name);
                if (childXSDAttribute.Required && childAttribute == null)
                {
                    Console.Error.WriteLine($"XSD: Attribute {childXSDAttribute.Name} is required on {Name} in {filename}");
                    Console.ReadKey();
                }
            }
        }
    }

    public class XSDAttribute
    {
        public string Name { get; set; } = String.Empty;

        public bool Required { get; set; }

        public List<String> Values { get; } = new List<String>();
    }
}
