using System;
using System.Xml.Linq;

namespace Webfuel.Tools.Datafuel
{
    public class SchemaEntityPropertyEnum : SchemaEntityPropertyPrimative
    {
        public SchemaEntityPropertyEnum(SchemaEntity entity, XElement element, bool nullable, string tag)
            : base(entity, element, new PrimativeEnum(nullable, tag))
        {
        }

        public bool Flags
        {
            get { return ((PrimativeEnum)Primative).Flags; }
            set { ((PrimativeEnum)Primative).Flags = value; }
        }

        public override IEnumerable<string> GenerateValidationRules()
        {
            if (!Flags)
                yield return $".IsInEnum()" + (Nullable ? ".When(x => x != null, ApplyConditionTo.CurrentValidator)" : "");
        }
    }
}
