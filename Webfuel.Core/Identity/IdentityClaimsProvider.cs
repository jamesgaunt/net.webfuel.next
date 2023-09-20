namespace Webfuel
{
    public interface IIdentityClaimsProvider
    {
        Task ProvideIdentityClaims(IdentityUser user, IdentityClaims claims);
    }
}
