using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Common
{
    public interface IFileStorageService
    {
        Task<FileStorageGroup> CreateGroup();

        Task<FileStorageEntry> UploadFile(Guid fileStorageGroupId, IFormFile formFile);

        Task<List<FileStorageEntry>> ListFiles(Guid fileStorageGroupId);

        Task DeleteFile(Guid fileStorageEntryId);
    }

    [Service(typeof(IFileStorageService))]
    internal class FileStorageService: IFileStorageService
    {
        private readonly IFileStorageConfiguration _configuration;
        private readonly IFileStorageGroupRepository _fileStorageGroupRepository;
        private readonly IFileStorageEntryRepository _fileStorageEntryRepository;

        public FileStorageService(
            IFileStorageConfiguration configuration,
            IFileStorageGroupRepository fileStorageGroupRepository,
            IFileStorageEntryRepository fileStorageEntryRepository)
        {
            _configuration = configuration;
            _fileStorageGroupRepository = fileStorageGroupRepository;
            _fileStorageEntryRepository = fileStorageEntryRepository;
        }

        public async Task<FileStorageGroup> CreateGroup()
        {
            var entity = new FileStorageGroup();
            entity.CreatedAt = DateTimeOffset.UtcNow;
            return await _fileStorageGroupRepository.InsertFileStorageGroup(entity);
        }

        public async Task<FileStorageEntry> UploadFile(Guid fileStorageGroupId, IFormFile formFile)
        {
            var entry = new FileStorageEntry
            {
                FileName = formFile.FileName,
                SizeBytes = formFile.Length,
                FileStorageGroupId = fileStorageGroupId,
            };
            entry = await _fileStorageEntryRepository.InsertFileStorageEntry(entry);

            using (var fs = formFile.OpenReadStream())
            {
                await BlobStorage.UploadBlobAsync(Path(entry), fs);
            }

            entry.UploadedAt = DateTimeOffset.UtcNow;
            return await _fileStorageEntryRepository.UpdateFileStorageEntry(entry);
        }

        public async Task<List<FileStorageEntry>> ListFiles(Guid fileStorageGroupId)
        {
            return await _fileStorageEntryRepository.SelectFileStorageEntryByFileStorageGroupId(fileStorageGroupId);
        }

        public async Task DeleteFile(Guid fileStorageEntryId)
        {
            var entry = await _fileStorageEntryRepository.RequireFileStorageEntry(fileStorageEntryId);

            await _fileStorageEntryRepository.DeleteFileStorageEntry(entry.Id);
            await BlobStorage.DeleteBlobAsync(Path(entry));
        }

        // Implementation

        string Path(FileStorageEntry entry)
        {
            return $"/{entry.FileStorageGroupId}/{entry.Id}/{entry.FileName}";
        }


        BlobStorage BlobStorage
        {
            get
            {
                return _blobStorage ??= new BlobStorage(connectionString: _configuration.BlogStorageConnectionString, containerName: _configuration.BlobStorageContainerName);
            }
        }

        BlobStorage? _blobStorage = null;
    }
}
