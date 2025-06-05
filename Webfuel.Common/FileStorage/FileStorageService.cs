namespace Webfuel.Common;

public interface IFileStorageService
{
    Task<FileStorageGroup> CreateGroup();

    Task<QueryResult<FileStorageEntry>> QueryFiles(QueryFileStorageEntry query);

    Task<FileStorageEntry> UploadFile(UploadFileStorageEntry command);

    Task<FileStorageEntry> UpdateFile(UpdateFileStorageEntry command);

    Task DeleteFile(Guid fileStorageEntryId);

    Task<string> GenerateFileSasUri(Guid fileStorageEntryId);

    Task<MemoryStream?> LoadDirect(string path);
}

[Service(typeof(IFileStorageService))]
internal class FileStorageService : IFileStorageService
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

    public async Task<FileStorageEntry> UploadFile(UploadFileStorageEntry command)
    {
        if (command.FormFile == null)
            throw new InvalidOperationException("No form file supplied to upload file");

        var entry = new FileStorageEntry
        {
            FileName = command.FormFile.FileName,
            SizeBytes = command.FormFile.Length,
            FileStorageGroupId = command.FileStorageGroupId,
        };
        entry = await _fileStorageEntryRepository.InsertFileStorageEntry(entry);

        using (var fs = command.FormFile.OpenReadStream())
        {
            await BlobStorage.UploadBlobAsync(Path(entry), fs);
        }

        entry.UploadedAt = DateTimeOffset.UtcNow;
        entry.UploadedByUserId = _identityAccessor.User?.Id;

        return await _fileStorageEntryRepository.UpdateFileStorageEntry(entry);
    }

    public async Task<FileStorageEntry> UpdateFile(UpdateFileStorageEntry command)
    {
        var entry = await _fileStorageEntryRepository.RequireFileStorageEntry(command.Id);
        var updated = entry.Copy();
        updated.FileTagIds = command.FileTagIds;
        updated = await _fileStorageEntryRepository.UpdateFileStorageEntry(original: entry, updated: updated);
        await CalculateFileStorageGroupTags(entry.FileStorageGroupId);
        return updated;
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
        await CalculateFileStorageGroupTags(entry.FileStorageGroupId);
    }

    public async Task<MemoryStream?> LoadDirect(string path)
    {
        var blob = await BlobStorage.GetBlobAsync(path);
        if (blob == null)
            return null;

        var stream = new MemoryStream();
        await blob.DownloadAsync(stream);
        stream.Position = 0;

        return stream;
    }

    async Task CalculateFileStorageGroupTags(Guid fileStorageGroupId)
    {
        var entries = await _fileStorageEntryRepository.SelectFileStorageEntryByFileStorageGroupId(fileStorageGroupId);
        var tags = entries.SelectMany(x => x.FileTagIds).Distinct().ToList();
        var group = await _fileStorageGroupRepository.RequireFileStorageGroup(fileStorageGroupId);
        group.FileTagIds = tags;
        await _fileStorageGroupRepository.UpdateFileStorageGroup(group);
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
