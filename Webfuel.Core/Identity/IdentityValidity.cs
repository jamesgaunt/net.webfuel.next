namespace Webfuel
{
    [ApiType]
    public class IdentityValidity
    {
        public DateTimeOffset ValidUntil { get; set; }

        public string ValidFromIPAddress { get; set; } = String.Empty;
    }
}
