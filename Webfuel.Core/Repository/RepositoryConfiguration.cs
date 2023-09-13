using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel
{
    public interface IRepositoryConfiguration
    {
        string DatabaseSchema { get; }

        string ConnectionString { get; }
    }



    [ServiceImplementation(typeof(IRepositoryConfiguration))]
    internal class RepositoryConfiguration: IRepositoryConfiguration
    {
        public RepositoryConfiguration(IConfiguration configuration)
        {
            DatabaseSchema = configuration["Webfuel:Repository:DatabaseSchema"];
            ConnectionString = configuration["Webfuel:Repository:ConnectionString"];
        }

        public string DatabaseSchema { get; private set; }

        public string ConnectionString { get; private set; }
    }
}
