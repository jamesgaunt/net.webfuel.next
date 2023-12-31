﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Webfuel.Tools.Datafuel
{
    public abstract class SchemaEntityMember : SchemaElement
    {
        public SchemaEntityMember(SchemaEntity entity, XElement element)
            : base(entity.Schema, element)
        {
            Entity = entity;
            Name = element.StringProperty("Name") ?? throw new InvalidOperationException();
            Default = element.StringProperty("Default");
            Access = element.StringProperty("Access") ?? "public";
            Tags.AddRange((element.StringProperty("Tags") ?? String.Empty).Split('|').Select(p => p.Trim()));
        }

        public SchemaEntity Entity { get; private set; }

        public string Name { get; private set; }

        public abstract bool Nullable { get; }

        public string? Default { get; private set; }

        public string Access { get; private set; }

        public bool IsKey { get { return Element.Name == "Key"; } }

        // Tags

        List<string> Tags { get; } = new List<string>();

        bool HasTag(string tag) => Tags.Contains(tag);

        public bool JsonIgnore => HasTag("JsonIgnore");

        public bool InternalSet => HasTag("InternalSet") || Entity.StaticData;

        public string TagParameter(string tagPrefix)
        {
            foreach(var tag in Tags)
            {
                if (tag.StartsWith(tagPrefix + ":"))
                    return tag.Substring(tagPrefix.Length + 1);
            }
            return String.Empty;
        }

        // Type

        public abstract string CLRType { get; }

        public virtual string CLRTypeWithNullable => CLRType + (Nullable ? "?" : "");

        public abstract string SQLType { get; }

        public abstract object? ParseValue(string? input);

        // Generators

        public abstract void GenerateEntityProperty(ScriptBuilder sb);

        public abstract string GenerateSqlColumnDefinition();

        public virtual void GenerateSqlConstraintDefinition(ScriptBuilder sb)
        {

        }

        public virtual void GenerateCoercion(ScriptBuilder sb)
        {
        }

        public virtual IEnumerable<string> GenerateValidationMetadata()
        {
            yield break;
        }

        public virtual IEnumerable<string> GenerateValidationRules()
        {
            yield break;
        }

        public virtual void GenerateCopy(ScriptBuilder sb)
        {
            sb.WriteLine($"entity.{Name} = {Name};");
        }

        public virtual string GenerateRepositoryGetter(string prefix = "entity.")
        {
            if (Nullable)
                return $"{prefix}{Name} ?? (object?)DBNull.Value";
            else
                return $"{prefix}{Name}";
        }

        public virtual string GenerateRepositorySetter(string prefix = "entity.")
        {
            if (Nullable)
                return $"{prefix}{Name} = value == DBNull.Value ? ({CLRTypeWithNullable})null : ({CLRTypeWithNullable})value;";
            else
                return $"{prefix}{Name} = ({CLRType})value!;";
        }

        public virtual string GenerateComparisonGetter(string entity)
        {
            return $"{entity}.{Name}";
        }

        public virtual string GenerateSqlComparison()
        {
            var sql = $"{Name} = @{Name}";
            if (Nullable)
                sql = $"(({sql}) OR ({Name} IS NULL AND @{Name} IS NULL))";
            return sql;
        }
    }
}
