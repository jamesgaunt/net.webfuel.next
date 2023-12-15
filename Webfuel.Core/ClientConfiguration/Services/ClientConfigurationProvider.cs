
namespace Webfuel
{
    public interface IClientConfigurationProvider
    {
        Task ProvideClientConfiguration(ClientConfiguration clientConfiguration);
    }
}
