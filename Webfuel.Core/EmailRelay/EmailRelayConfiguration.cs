using Microsoft.Extensions.Configuration;

namespace Webfuel
{
    public interface IEmailRelayConfiguration
    {
        string StorageConnectionString { get; }

        string QueueName { get; }

        string ContainerName { get; }

        string SystemName { get; }
    }

    [Service(typeof(IEmailRelayConfiguration))]
    internal class EmailRelayConfiguration : IEmailRelayConfiguration
    {
        public EmailRelayConfiguration(IConfiguration configuration)
        {
            StorageConnectionString = configuration["Webfuel:EmailRelay:StorageConnectionString"] ?? String.Empty;
            QueueName = configuration["Webfuel:EmailRelay:QueueName"] ?? String.Empty; ;
            ContainerName = configuration["Webfuel:EmailRelay:ContainerName"] ?? String.Empty; ;
            SystemName = configuration["Webfuel:EmailRelay:SystemName"] ?? String.Empty; ;
        }

        public string StorageConnectionString { get; private set; }

        public string QueueName { get; private set; }

        public string ContainerName { get; private set; }

        public string SystemName { get; private set; }
    }
}
