using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Webfuel.Tools.Datafuel
{
    public partial class Schema
    {
        public List<SchemaEntity> Entities { get; } = new List<SchemaEntity>();

        public SchemaEntity FindEntity(string entityName)
        {
            // Little helper so we can search directly on reference entity attributes
            entityName = (entityName ?? String.Empty).Replace("?", "");

            var entity = Entities.Where(p => p.Name == entityName).FirstOrDefault();
            if (entity == null)
                throw new InvalidOperationException("The entity " + entityName + " does not exist");
            return entity;
        }

        public static Schema Load()
        {
            var definitions = LoadDefinitions(Settings.DefinitionRoot);

            if (definitions.Count() == 0)
                throw new InvalidOperationException("No definitions found");

            var schema = new Schema();

            foreach (var definition in definitions)
            {
                foreach (var element in definition.Root!.Elements("Entity"))
                {
                    schema.Entities.Add(new SchemaEntity(schema, element));
                }
            }

            return schema;
        }

        static List<XDocument> LoadDefinitions(string definitionPath)
        {
            var result = new List<XDocument>();

            foreach(var directory in Directory.GetDirectories(definitionPath))
            {
                foreach(var filename in Directory.GetFiles(directory, "*.xml"))
                {
                    var xml = XDocument.Parse(File.ReadAllText(filename));
                    XSD.Validate(xml.Root!, Path.GetFileName(filename));
                    xml.Root!.Add(new XAttribute("filename", filename));
                    result.Add(xml);
                }
            }
            return result;
        }
    }
}
