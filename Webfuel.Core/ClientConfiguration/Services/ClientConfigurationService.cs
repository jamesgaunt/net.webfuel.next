using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel
{ 
    public interface IClientConfigurationService: IClientConfigurationProvider
    {
    }

    [Service(typeof(IClientConfigurationService), typeof(IClientConfigurationProvider))]    
    internal class ClientConfigurationService: IClientConfigurationService
    {
        private readonly IIdentityAccessor _identityAccessor;

        public ClientConfigurationService(IIdentityAccessor identityAccessor)
        {
            _identityAccessor = identityAccessor;
        }

        public Task ProvideClientConfiguration(ClientConfiguration clientConfiguration)
        {
            if (_identityAccessor.User == null)
                throw new NotAuthenticatedException();

            clientConfiguration.UserId = _identityAccessor.User.Id;
            clientConfiguration.Email = _identityAccessor.User.Email;
            clientConfiguration.Claims = _identityAccessor.Claims;

            return Task.CompletedTask;
        }
    }
}
