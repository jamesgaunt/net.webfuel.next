namespace Webfuel
{
    [ApiType]
    public class IdentityClaims
    {
        public bool IsDeveloper { get; set; }

        public bool CanAccessUsers { get; set; }

        public string SomeStringClaim { get; set; } = String.Empty;
    }
}
