using System;
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
            JsonIgnore = element.BooleanProperty("JsonIgnore") ?? false;
            Access = element.StringProperty("Access") ?? "public";
        }

        public SchemaEntity Entity { get; private set; }

        public string Name { get; private set; }

        public abstract bool Nullable { get; }

        public string? Default { get; private set; }

        public bool JsonIgnore { get; private set; }

        public string Access { get; private set; }

        public bool IsKey { get { return Element.Name == "Key"; } }

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

        public virtual void GenerateValidation(ScriptBuilder sb)
        {
        }

        public virtual void GenerateCopy(ScriptBuilder sb)
        {
            sb.WriteLine($"entity.{Name} = {Name};");
        }

        public virtual void GenerateRepositoryGetter(ScriptBuilder sb)
        {
            sb.WriteLine($"return entity.{Name};");
        }

        public virtual void GenerateRepositorySetter(ScriptBuilder sb)
        {
            if (Nullable)
                sb.WriteLine($"entity.{Name} = value == DBNull.Value ? ({CLRTypeWithNullable})null : ({CLRTypeWithNullable})value;");
            else
                sb.WriteLine($"entity.{Name} = ({CLRType})value!;");
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
