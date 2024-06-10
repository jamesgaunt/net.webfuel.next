using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Webfuel.Tools.Datafuel
{
    public class SchemaEntityQuery : SchemaElement
    {
        public SchemaEntityQuery(SchemaEntity entity, XElement element)
            : base(entity.Schema, element)
        {
            Entity = entity;

            Name = element.StringProperty("Name") ?? throw new NotImplementedException();
            ReturnType = element.StringProperty("ReturnType") ?? String.Empty;

            if (Name.StartsWith("Select") || Name.StartsWith("Pick"))
                Shape = QueryShape.Select;
            else if (Name.StartsWith("Get"))
                Shape = QueryShape.Get;
            else if (Name.StartsWith("Count"))
                Shape = QueryShape.Count;

            foreach (var parameter in element.Elements("Parameter"))
                Parameters.Add(new SchemaEntityQueryParameter(this, parameter));

            Sql = element.Element("Sql")!.Value;
        }

        public SchemaEntity Entity { get; private set; }

        public string Name { get; private set; }

        public string ReturnType { get; private set; }

        public QueryShape Shape { get; private set; } = QueryShape.Unknown;

        public string Sql { get; private set; }

        public List<SchemaEntityQueryParameter> Parameters { get; } = new List<SchemaEntityQueryParameter>();

        // Generators

        public string GenerateSignature()
        {
            return String.Join(", ", Parameters.Select(p => $"{p.GenerateCLRType()} {p.Name.Replace("@", "").ToCamelCase()}"));
        }

        public string GenerateArguments()
        {
            return String.Join(", ", Parameters.Select(p => $"{p.Name.Replace("@", "").ToCamelCase()}"));
        }

        public string GenerateResultType()
        {
            switch(Shape)
            {
                case QueryShape.Get:
                    return $"{Entity.Name}?";
                case QueryShape.Select:
                    return $"List<{Entity.Name}>";
                case QueryShape.Count:
                    return "int";
                default:
                    if (String.IsNullOrEmpty(ReturnType))
                        return "object?";
                    return ReturnType;
            }
        }

        public string MapReturnType()
        {
            if (String.IsNullOrEmpty(ReturnType))
                return "result";

            var primative = Primative.Build(Schema, ReturnType);

            if (primative.Nullable)
                return $"result == DBNull.Value ? null : ({primative.CLRType})result";
            return $"({primative.CLRType})result";
        }
    }

    public class SchemaEntityQueryParameter : SchemaElement
    {
        public SchemaEntityQueryParameter(SchemaEntityQuery query, XElement element)
            : base(query.Schema, element)
        {
            Name = element.StringProperty("Name") ?? throw new NotImplementedException();
            Collection = element.BooleanProperty("Collection") ?? false;
        }

        public string Name { get; private set; }

        public bool Collection { get; private set; }

        public Primative Primative
        {
            get
            {
                if(_primative == null)
                    _primative = Primative.Build(Schema, Element.StringProperty("Type"));
                return _primative;
            }
        }
        Primative? _primative = null;

        public string GenerateCLRType()
        {
            var type = Primative.CLRTypeWithNullable;
            if (Collection)
                type = $"IEnumerable<{type}>";
            return type;
        }
    }
}
