using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Common
{
    public interface IConfigurationService
    {
        Task<int> AllocateNextProjectNumber();

        Task<int> AllocateNextSupportRequestNumber();

        Task<Configuration> GetConfiguration(bool reload = false);
    }

    [Service(typeof(IConfigurationService))]
    internal class ConfigurationService: IConfigurationService
    {
        private readonly IConfigurationRepository _configurationRepository;

        public ConfigurationService(IConfigurationRepository configurationRepository)
        {
            _configurationRepository = configurationRepository;
        }

        public async Task<int> AllocateNextProjectNumber()
        {
            var _configuration = await GetConfiguration(true);

            var nextProjectNumber = _configuration.NextProjectNumber;

            var original = _configuration.Copy();
            _configuration.NextProjectNumber += 1;

            await _configurationRepository.UpdateConfiguration(original: original, updated: _configuration);

            return nextProjectNumber;
        }

        public async Task<int> AllocateNextSupportRequestNumber()
        {
            var _configuration = await GetConfiguration(true);

            var nextSupportRequestNumber = _configuration.NextSupportRequestNumber;

            var original = _configuration.Copy();
            _configuration.NextSupportRequestNumber += 1;

            await _configurationRepository.UpdateConfiguration(original: original, updated: _configuration);

            return nextSupportRequestNumber;
        }

        public async Task<Configuration> GetConfiguration(bool reload = false)
        {
            if (reload || _configuration == null)
                _configuration = await _configurationRepository.RequireConfiguration(Guid.Empty);
            return _configuration.Copy();
        }

        static Configuration? _configuration = null;
    }
}
