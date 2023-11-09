using Microsoft.Extensions.Configuration;

namespace Webfuel
{
    public interface IFileStorageConfiguration
    {
        string BlobStorageContainerName { get; }

        string BlobStorageContainerUrl { get; }

        string BlogStorageConnectionString { get; }
    }

    [Service(typeof(IFileStorageConfiguration))]
    internal class FileStorageConfiguration : IFileStorageConfiguration
    {
        public FileStorageConfiguration(IConfiguration configuration)
        {
            BlobStorageContainerName = configuration["Webfuel:FileStorage:BlobStorageContainerName"] ?? String.Empty;
            BlobStorageContainerUrl = configuration["Webfuel:FileStorage:BlobStorageContainerUrl"] ?? String.Empty;
            BlogStorageConnectionString = configuration["Webfuel:FileStorage:BlogStorageConnectionString"] ?? String.Empty;
        }

        public string BlobStorageContainerName { get; private set; }

        public string BlobStorageContainerUrl { get; private set; }

        public string BlogStorageConnectionString { get; private set; }
    }
}
