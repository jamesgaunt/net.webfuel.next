using System.Security.Cryptography;

namespace Webfuel.Common;

public interface IFileStorageService
{
    Task<FileStorageGroup> CreateGroup();

    Task<QueryResult<FileStorageEntry>> QueryFiles(QueryFileStorageEntry query);

    Task<FileStorageEntry> UploadFile(UploadFileStorageEntry command);

    Task<FileStorageEntry> UpdateFile(UpdateFileStorageEntry command);

    Task DeleteFile(Guid fileStorageEntryId);

    Task<string> GenerateFileSasUri(Guid fileStorageEntryId);

    Task<string> GenerateFileAccessToken(Guid fileStorageEntryId);

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

    public async Task<string> GenerateFileAccessToken(Guid fileStorageEntryId)
    {
        var entry = await _fileStorageEntryRepository.RequireFileStorageEntry(fileStorageEntryId);
        var token = Encrypt(entry.Id.ToString("N").ToLowerInvariant(), "FILE_ACCESS_TOKEN");
        return token;
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

    // Encryption and Decryption

    /// <summary>
    /// Encrypts the plainText input using the given Key.
    /// A 128 bit random salt will be generated and prepended to the ciphertext before it is base64 encoded.
    /// </summary>
    /// <param name="plainText">The plain text to encrypt.</param>
    /// <param name="key">The plain text encryption key.</param>
    /// <returns>The salt and the ciphertext, Base64 encoded for convenience.</returns>
    string Encrypt(string plainText, string key)
    {
        var saltSize = 32;

        if (string.IsNullOrEmpty(plainText))
            throw new ArgumentNullException("plainText");
        if (string.IsNullOrEmpty(key))
            throw new ArgumentNullException("key");

        // Derive a new Salt and IV from the Key
        using (var keyDerivationFunction = new Rfc2898DeriveBytes(key, saltSize, 2, HashAlgorithmName.SHA256))
        {
            var saltBytes = keyDerivationFunction.Salt;
            var keyBytes = keyDerivationFunction.GetBytes(32);
            var ivBytes = keyDerivationFunction.GetBytes(16);

            // Create an encryptor to perform the stream transform.
            // Create the streams used for encryption.
            using (var aesManaged = Aes.Create())
            using (var encryptor = aesManaged.CreateEncryptor(keyBytes, ivBytes))
            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                using (var streamWriter = new StreamWriter(cryptoStream))
                {
                    // Send the data through the StreamWriter, through the CryptoStream, to the underlying MemoryStream
                    streamWriter.Write(plainText);
                }

                // Return the encrypted bytes from the memory stream, in Base64 form so we can send it right to a database (if we want).
                var cipherTextBytes = memoryStream.ToArray();
                Array.Resize(ref saltBytes, saltBytes.Length + cipherTextBytes.Length);
                Array.Copy(cipherTextBytes, 0, saltBytes, saltSize, cipherTextBytes.Length);

                return Convert.ToBase64String(saltBytes);
            }
        }
    }

    /// <summary>
    /// Decrypts the ciphertext using the Key.
    /// </summary>
    /// <param name="cipherText">The ciphertext to decrypt.</param>
    /// <param name="key">The plain text encryption key.</param>
    /// <returns>The decrypted text.</returns>
    string Decrypt(string cipherText, string key)
    {
        var saltSize = 32;

        if (string.IsNullOrEmpty(cipherText))
            throw new ArgumentNullException("cipherText");
        if (string.IsNullOrEmpty(key))
            throw new ArgumentNullException("key");

        // Extract the salt from our ciphertext
        var allTheBytes = Convert.FromBase64String(cipherText);
        var saltBytes = allTheBytes.Take(saltSize).ToArray();
        var ciphertextBytes = allTheBytes.Skip(saltSize).Take(allTheBytes.Length - saltSize).ToArray();

        using (var keyDerivationFunction = new Rfc2898DeriveBytes(key, saltBytes, 2, HashAlgorithmName.SHA256))
        {
            // Derive the previous IV from the Key and Salt
            var keyBytes = keyDerivationFunction.GetBytes(32);
            var ivBytes = keyDerivationFunction.GetBytes(16);

            // Create a decrytor to perform the stream transform.
            // Create the streams used for decryption.
            // The default Cipher Mode is CBC and the Padding is PKCS7 which are both good
            using (var aesManaged = Aes.Create())
            using (var decryptor = aesManaged.CreateDecryptor(keyBytes, ivBytes))
            using (var memoryStream = new MemoryStream(ciphertextBytes))
            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
            using (var streamReader = new StreamReader(cryptoStream))
            {
                // Return the decrypted bytes from the decrypting stream.
                return streamReader.ReadToEnd();
            }
        }
    }
}
