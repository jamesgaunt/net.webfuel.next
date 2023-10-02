using Microsoft.Extensions.Configuration;

namespace Webfuel
{
    public interface IRepositoryConfiguration
    {
        string ConnectionString { get; }
    }

    [Service(typeof(IRepositoryConfiguration))]
    internal class RepositoryConfiguration : IRepositoryConfiguration
    {
        public RepositoryConfiguration(IConfiguration configuration)
        {
            ConnectionString = configuration["Webfuel:Repository:ConnectionString"] ?? String.Empty;
        }

        public string ConnectionString { get; private set; }
    }
}
