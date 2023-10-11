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
            var identityUser = _identityAccessor.User;
            if (identityUser == null)
                throw new NotAuthenticatedException();

            clientConfiguration.Email = identityUser.Email;

            return Task.CompletedTask;
        }
    }
}
