namespace Webfuel
{
    [ApiIgnore]
    public class IdentityToken
    {
        public required IdentityUser User { get; set; }

        public required IdentityClaims Claims { get; set; }

        public required IdentityValidity Validity { get; set; }

        public required string Signature { get; set; }
    }
}
