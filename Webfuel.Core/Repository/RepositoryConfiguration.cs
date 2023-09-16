using Microsoft.Extensions.Configuration;

namespace Webfuel
{
    public interface IRepositoryConfiguration
    {
        string DatabaseSchema { get; }

        string ConnectionString { get; }
    }



    [ServiceImplementation(typeof(IRepositoryConfiguration))]
    internal class RepositoryConfiguration : IRepositoryConfiguration
    {
        public RepositoryConfiguration(IConfiguration configuration)
        {
            DatabaseSchema = configuration["Webfuel:Repository:DatabaseSchema"] ?? String.Empty;
            ConnectionString = configuration["Webfuel:Repository:ConnectionString"] ?? String.Empty;
        }

        public string DatabaseSchema { get; private set; }

        public string ConnectionString { get; private set; }
    }
}
