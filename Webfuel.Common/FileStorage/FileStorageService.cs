using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Common
{
    public interface IFileStorageService
    {
        Task<FileStorageGroup> CreateGroup();

        Task<QueryResult<FileStorageEntry>> QueryFiles(QueryFileStorageEntry query);

        Task<FileStorageEntry> UploadFile(UploadFileStorageEntry upload);

        Task DeleteFile(Guid fileStorageEntryId);

        Task<string> GenerateFileSasUri(Guid fileStorageEntryId);
    }

    [Service(typeof(IFileStorageService))]
    internal class FileStorageService: IFileStorageService
    {
        private readonly IFileStorageConfiguration _configuration;
        private readonly IFileStorageGroupRepository _fileStorageGroupRepository;
        private readonly IFileStorageEntryRepository _fileStorageEntryRepository;
        private readonly IIdentityAccessor _identityAccessor;

        public FileStorageService(
            IFileStorageConfiguration configuration,
            IFileStorageGroupRepository fileStorageGroupRepository,
            IFileStorageEntryRepository fileStorageEntryRepository,
            IIdentityAccessor identityAccessor)
        {
            _configuration = configuration;
            _fileStorageGroupRepository = fileStorageGroupRepository;
            _fileStorageEntryRepository = fileStorageEntryRepository;
            _identityAccessor = identityAccessor;
        }

        public async Task<FileStorageGroup> CreateGroup()
        {
            var entity = new FileStorageGroup();
            entity.CreatedAt = DateTimeOffset.UtcNow;
            return await _fileStorageGroupRepository.InsertFileStorageGroup(entity);
        }

        public async Task<FileStorageEntry> UploadFile(UploadFileStorageEntry upload)
        {
            if (upload.FormFile == null)
                throw new InvalidOperationException("No form file supplied to upload file");

            var entry = new FileStorageEntry
            {
                FileName = upload.FormFile.FileName,
                SizeBytes = upload.FormFile.Length,
                FileStorageGroupId = upload.FileStorageGroupId,
            };
            entry = await _fileStorageEntryRepository.InsertFileStorageEntry(entry);

            using (var fs = upload.FormFile.OpenReadStream())
            {
                await BlobStorage.UploadBlobAsync(Path(entry), fs);
            }

            entry.UploadedAt = DateTimeOffset.UtcNow;
            entry.UploadedByUserId = _identityAccessor.User?.Id;

            return await _fileStorageEntryRepository.UpdateFileStorageEntry(entry);
        }

        public async Task<QueryResult<FileStorageEntry>> QueryFiles(QueryFileStorageEntry query)
        {
            return await _fileStorageEntryRepository.QueryFileStorageEntry(query.ApplyCustomFilters());
        }

        public async Task<string> GenerateFileSasUri(Guid fileStorageEntryId)
        {
            var entry = await _fileStorageEntryRepository.RequireFileStorageEntry(fileStorageEntryId);

            var uri = BlobStorage.GenerateSasUri(Path(entry));
            return uri.ToString();
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
