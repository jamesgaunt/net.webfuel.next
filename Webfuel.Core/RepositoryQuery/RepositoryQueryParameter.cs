namespace Webfuel
{
    public class RepositoryQueryParameter
    {
        public RepositoryQueryParameter(string name, object? value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }

        public object? Value { get; set; }
    }
}
