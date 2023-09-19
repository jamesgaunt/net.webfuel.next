namespace Webfuel
{
    [ApiType]
    public class IdentityToken
    {
        public required Identity Identity { get; set; }

        public required IdentityClaims Claims { get; set; }

        public required IdentityValidity Validity { get; set; }

        public required string Signature { get; set; }

        // This key must remain in sync with the API
        public static string Key { get; } = "IDENTITY_TOKEN";
    }
}
