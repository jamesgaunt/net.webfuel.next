using FluentValidation;
using MediatR;

namespace Webfuel.Domain.Common
{
    public class GetClientConfiguration : IRequest<ClientConfiguration>
    {
    }

    internal class GetClientConfigurationHandler : IRequestHandler<GetClientConfiguration, ClientConfiguration>
    {
        private readonly IIdentityAccessor _identityAccessor;

        public GetClientConfigurationHandler(IIdentityAccessor identityAccessor)
        {
            _identityAccessor = identityAccessor;
        }

        public Task<ClientConfiguration> Handle(GetClientConfiguration request, CancellationToken cancellationToken)
        {
            var identityUser = _identityAccessor.User;
            if (identityUser == null)
                throw new NotAuthenticatedException();

            var identityClaims = _identityAccessor.Claims;
            if (identityClaims == null)
                throw new NotAuthenticatedException();

            return Task.FromResult(new ClientConfiguration(identityUser, identityClaims));
        }
    }
}
