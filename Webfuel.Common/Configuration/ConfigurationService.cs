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
            var _configuration = await GetConfiguration();

            var nextProjectNumber = _configuration.NextProjectNumber;

            _configuration.NextProjectNumber++;
            await _configurationRepository.UpdateConfiguration(_configuration);

            return nextProjectNumber;
        }

        async Task<Configuration> GetConfiguration()
        {
            if (_configuration != null)
                return _configuration;

            _configuration = await _configurationRepository.GetConfiguration(ConfigurationId);
            if(_configuration != null)
                return _configuration;

            _configuration = await _configurationRepository.InsertConfiguration(new Configuration { Id = ConfigurationId });
            return _configuration;
        }

        static readonly Guid ConfigurationId = Guid.Parse("ad45744d-4630-489f-9e2b-94613752c516");

        static Configuration? _configuration = null;
    }
}
