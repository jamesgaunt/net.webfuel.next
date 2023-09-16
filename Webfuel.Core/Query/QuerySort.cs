namespace Webfuel
{
    [TypefuelInterface]
    public class QuerySort
    {
        public string Field { get; set; } = String.Empty;

        public int Direction { get; set; }
    }
}
