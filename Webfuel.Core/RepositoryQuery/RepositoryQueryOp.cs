namespace Webfuel
{
    public static class RepositoryQueryOp
    {
        public const string None = "none";

        public const string Equal = "eq";
        public const string NotEqual = "neq";

        public const string GreaterThan = "gt";
        public const string GreaterThanOrEqual = "gte";

        public const string LessThan = "lt";
        public const string LessThanOrEqual = "lte";

        public const string Contains = "contains";
        public const string StartsWith = "startswith";
        public const string EndsWith = "endswith";

        public const string And = "and";
        public const string Or = "or";

        public static string? ParseOp(string input)
        {
            if (string.IsNullOrEmpty(input))
                return null;

            input = input.ToLower();

            switch (input)
            {
                case "&&":
                case "and":
                    return And;

                case "||":
                case "or":
                    return Or;

                case "<":
                case "lt":
                    return LessThan;

                case ">":
                case "gt":
                    return GreaterThan;

                case "<=":
                case "lte":
                    return LessThanOrEqual;

                case ">=":
                case "gte":
                    return GreaterThanOrEqual;

                case "contains":
                    return Contains;

                case "startswith":
                    return StartsWith;

                case "endswith":
                    return EndsWith;

                case "=":
                case "==":
                    return Equal;

                case "!=":
                case "<>":
                    return NotEqual;
            }

            return input;
        }
    }
}
