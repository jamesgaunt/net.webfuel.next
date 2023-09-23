namespace Webfuel
{
    [ApiIgnore]
    public class IdentityClaims
    {
        public bool Developer { get; set; }

        public bool CanEditUsers { get; set; } = true;
    }
}
