using System;
using System.Xml.Linq;

namespace Webfuel.Tools.Datafuel
{
    public class SchemaEntityPropertyString : SchemaEntityPropertyPrimative
    {
        public SchemaEntityPropertyString(SchemaEntity entity, XElement element, bool nullable, string tag)
            : base(entity, element, new PrimativeString(nullable, (int?)ParseMinMaxTag(tag).max))
        {
            var minMax = ParseMinMaxTag(tag);
            MinLength = (int?)minMax.min;
            MaxLength = (int?)minMax.max;
            Trim = element.BooleanProperty("Trim") ?? true;
            CaseSensitive = element.BooleanProperty("CaseSensitive") ?? false;
        }

        public int? MinLength { get; private set; }

        public int? MaxLength { get; private set; }

        public bool Trim { get; private set; }

        // Generators

        public override IEnumerable<string> GenerateValidationMetadata()
        {
            if (MinLength.HasValue)
                yield return $"public const int {Name}_MinLength = {MinLength};";
            if (MaxLength.HasValue)
                yield return $"public const int {Name}_MaxLength = {MaxLength};";
        }

        public override IEnumerable<string> GenerateValidationRules()
        {
            if (!Nullable)
                yield return ".NotNull()";

            if (MinLength.HasValue)
                yield return $".MinimumLength({Name}_MinLength).When(x => x != null, ApplyConditionTo.CurrentValidator)";

            if (MaxLength.HasValue)
                yield return $".MaximumLength({Name}_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator)";

        }

        public override void GenerateCoercion(ScriptBuilder sb)
        {
            if (!Nullable)
                sb.WriteLine($"entity.{Name} = entity.{Name} ?? String.Empty;");
            if(Trim)
                sb.WriteLine($"entity.{Name} = entity.{Name}{(Nullable ? "?" : "")}.Trim();");
        }
    }
}
