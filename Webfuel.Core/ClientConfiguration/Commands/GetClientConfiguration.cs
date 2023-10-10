using FluentValidation;
using MediatR;

namespace Webfuel
{
    public class GetClientConfiguration : IRequest<ClientConfiguration>
    {
    }

    internal class GetClientConfigurationHandler : IRequestHandler<GetClientConfiguration, ClientConfiguration>
    {

        private readonly IEnumerable<IClientConfigurationProvider> _clientConfigurationProviders;

        public GetClientConfigurationHandler(
            IEnumerable<IClientConfigurationProvider> clientConfigurationProviders
            )
        {
            _clientConfigurationProviders = clientConfigurationProviders;
        }

        public async Task<ClientConfiguration> Handle(GetClientConfiguration request, CancellationToken cancellationToken)
        {
            var clientConfiguration = new ClientConfiguration();

            foreach (var clientConfigurationProvider in _clientConfigurationProviders)
                await clientConfigurationProvider.ProvideClientConfiguration(clientConfiguration);

            return clientConfiguration;
        }
    }
}
