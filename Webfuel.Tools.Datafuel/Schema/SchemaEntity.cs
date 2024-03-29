﻿using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Webfuel.Tools.Datafuel
{
    public class SchemaEntity : SchemaElement
    {
        public SchemaEntity(Schema schema, XElement element)
            : base(schema, element)
        {
            Name = element.StringProperty("Name") ?? throw new InvalidOperationException();

            Repository = element.BooleanProperty("Repository") ?? true;
            Interface = element.StringProperty("Interface") ?? String.Empty;
            OrderBy = element.StringProperty("OrderBy") ?? String.Empty;
            Tags.AddRange((element.StringProperty("Tags") ?? String.Empty).Split('|').Select(p => p.Trim()));

            if (element.Elements().Any(p => p.Name == "Key"))
                Key = SchemaEntityProperty.Build(this, element.Element("Key")!);

            foreach (var index in element.Elements("Index"))
                Indexes.Add(new SchemaEntityIndex(this, index));

            foreach (var query in element.Elements("Query"))
                Queries.Add(new SchemaEntityQuery(this, query));

            if (element.Elements().Any(p => p.Name == "View"))
                View = new SchemaEntityView(this, element.Element("View")!);

            GenerateStandardQueries();

            if (Key == null)
                KeyType = RepositoryKeyType.None;
            else if (Key.CLRType == "Guid")
                KeyType = RepositoryKeyType.CombGuid;
            else
                KeyType = RepositoryKeyType.Other;

            if (element.Elements("Data").Count() == 1)
                Data = new SchemaData(this, element.Element("Data")!);

            var filename = element.Document!.Root!.Attribute("filename")?.Value;
            if (filename != null && filename.Contains(Settings.DefinitionRoot))
            {
                filename = filename.Replace(Settings.DefinitionRoot, "");
                Assembly = filename.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries)[0];
            }
        }

        public RepositoryKeyType KeyType { get; private set; }

        // Properties

        public string Name { get; private set; }

        public SchemaEntityProperty? Key { get; private set; }

        public List<SchemaEntityProperty> Properties
        {
            get
            {
                if (_properties == null)
                    BuildMembers();
                return _properties!;
            }
        }
        List<SchemaEntityProperty>? _properties = null;

        public List<SchemaEntityReference> References
        {
            get
            {
                if (_references == null)
                    BuildMembers();
                return _references!;
            }
        }
        List<SchemaEntityReference>? _references = null;

        void BuildMembers()
        {
            _properties = new List<SchemaEntityProperty>();
            _references = new List<SchemaEntityReference>();

            foreach (var property in Element.Elements("Property"))
            {
                var p = SchemaEntityProperty.Build(this, property);
                if (p != null)
                {
                    _properties.Add(p);
                }
                else
                {
                    // Its not a property so it had better be a reference
                    _references.Add(new SchemaEntityReference(this, property));
                }
            }

            foreach (var reference in Element.Elements("Reference"))
            {
                _references.Add(new SchemaEntityReference(this, reference));
            }
        }

        public SchemaEntityView? View { get; private set; }

        public List<SchemaEntityIndex> Indexes { get; } = new List<SchemaEntityIndex>();

        public List<SchemaEntityQuery> Queries { get; } = new List<SchemaEntityQuery>();

        public IEnumerable<SchemaEntityMember> Members
        {
            get
            {
                if (Key != null)
                    yield return Key;
                foreach (var property in Properties)
                    yield return property;
                foreach (var reference in References)
                    yield return reference;
            }
        }

        public bool Repository { get; private set; } = true;

        public string Interface { get; private set; } = String.Empty;

        public string OrderBy { get; private set; } = String.Empty;

        public SchemaData? Data { get; private set; }

        public string Assembly { get; private set; } = String.Empty;

        public string Namespace
        {
            get
            {
                if (String.IsNullOrEmpty(Assembly) || Assembly == "Core")
                    return "Webfuel";
                return "Webfuel." + Assembly;
            }
        }

        public string GeneratedDirectory
        {
            get
            {
                return $"{Settings.SolutionRoot}\\Webfuel.{Assembly}\\_Generated";
            }
        }

        public string DefaultOrderBy
        {
            get
            {
                var orderBy = OrderBy.Replace(" ", "");
                
                if (String.IsNullOrEmpty(orderBy))
                {
                    if (Key != null)
                        return $"ORDER BY {Key.Name} ASC";
                    return String.Empty;
                }

                var terms = new List<string>();
                var fields = orderBy.Split(",");

                foreach(var field in fields)
                {
                    var name = field;
                    var descending = false;
                    if (name.StartsWith("-"))
                    {
                        name = field.Substring(1);
                        descending = true;
                    }
                    terms.Add($"{name}{(descending ? " DESC" : " ASC")}");
                }

                return $"ORDER BY {String.Join(", ", terms)}";
            }
        }

        // Tags

        List<string> Tags { get; } = new List<string>();

        bool HasTag(string tag) => Tags.Contains(tag);

        public bool Enum => HasTag("Enum");

        public bool JsonIgnore => HasTag("JsonIgnore");

        public bool StaticData => HasTag("StaticData");

        public bool ReadOnly => HasTag("ReadOnly") || Enum;

        public bool ChangeLog => HasTag("ChangeLog");

        // Methods

        public SchemaEntityMember FindMember(string? memberName)
        {
            var member = Members.Where(p => p.Name == memberName).FirstOrDefault();
            if (member == null)
                throw new InvalidOperationException("The member " + memberName + " does not exist on the entity " + Name);
            return member;
        }

        // Standard Queries

        void GenerateStandardQueries()
        {
            foreach (var index in Indexes)
                GenerateStandardQuery_ForIndex(index);

            GenerateStandardQuery_SelectWithPage();
            GenerateStandardQuery_Select();
            GenerateStandardQuery_Count();
            GenerateStandardQuery_Get();
        }

        void GenerateStandardQuery_Get()
        {
            if (Key == null)
                return;

            var name = $"Get{Name}";
            if (Queries.Any(p => p.Name == name))
                return; // A query with the same name already exists

            var element = new XElement("Query", new XAttribute("Name", name));
            element.Add(new XElement("Sql", $"SELECT * FROM [{Name}] WHERE {Key.Name} = @{Key.Name}"));
            element.Add(new XElement("Parameter", new XAttribute("Name", $"@{Key.Name}"), new XAttribute("Type", $"{Name}.{Key.Name}")));
            Queries.Insert(0, new SchemaEntityQuery(this, element));
        }

        void GenerateStandardQuery_Select()
        {
            var name = $"Select{Name}";
            if (Queries.Any(p => p.Name == name))
                return; // A query with the same name already exists

            var element = new XElement("Query", new XAttribute("Name", name));
            element.Add(new XElement("Sql", $"SELECT * FROM [{Name}] {DefaultOrderBy}"));
            Queries.Insert(0, new SchemaEntityQuery(this, element));
        }

        void GenerateStandardQuery_Count()
        {
            var name = $"Count{Name}";
            if (Queries.Any(p => p.Name == name))
                return; // A query with the same name already exists

            var element = new XElement("Query", new XAttribute("Name", name));
            element.Add(new XElement("Sql", $"SELECT COUNT(Id) FROM [{Name}]"));
            Queries.Insert(0, new SchemaEntityQuery(this, element));
        }


        void GenerateStandardQuery_SelectWithPage()
        {
            var name = $"Select{Name}WithPage";
            if (Queries.Any(p => p.Name == name))
                return; // A query with the same name already exists

            var element = new XElement("Query", new XAttribute("Name", name));
            element.Add(new XElement("Sql", $"SELECT * FROM [{Name}] {DefaultOrderBy} OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY"));
            element.Add(new XElement("Parameter", new XAttribute("Name", "@Skip"), new XAttribute("Type", "int32")));
            element.Add(new XElement("Parameter", new XAttribute("Name", "@Take"), new XAttribute("Type", "int32")));
            Queries.Insert(0, new SchemaEntityQuery(this, element));
        }

        void GenerateStandardQuery_ForIndex(SchemaEntityIndex index)
        {
            var name = $"{(index.Unique ? "Get" : "Select")}{Name}By{String.Join("And", index.Members.Select(p => p.Name))}";
            if (Queries.Any(p => p.Name == name))
                return; // A query with the same name already exists

            var element = new XElement("Query", new XAttribute("Name", name));
            element.Add(new XElement("Sql", $"SELECT * FROM [{Name}] WHERE {String.Join(" AND ", index.Members.Select(p => p.GenerateSqlComparison()))} {DefaultOrderBy}"));
            foreach (var member in index.Members)
                element.Add(new XElement("Parameter", new XAttribute("Name", $"@{member.Name}"), new XAttribute("Type", $"{Name}.{member.Name}")));
            Queries.Insert(0, new SchemaEntityQuery(this, element));
        }
    }
}
