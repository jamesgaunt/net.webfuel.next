using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Webfuel
{
    public class Blob
    {
        private readonly BlobContainerClient Container;
        private readonly BlobItem Item;

        internal Blob(BlobContainerClient container, BlobItem item)
        {
            Container = container;
            Item = item;
            Path = Item.Name.StartsWith("/") ? Item.Name : "/" + Item.Name;
        }

        public string Path
        {
            get; private set;
        }

        public long Length
        {
            get { return Item.Properties.ContentLength ?? 0; }
        }

        public DateTime LastModifiedUtc
        {
            get { return Item.Properties.LastModified?.UtcDateTime ?? DateTime.MinValue; }
        }

        public async Task DownloadAsync(Stream stream)
        {
            await Container.GetBlobClient(Item.Name).DownloadToAsync(stream);
        }
    }

    public class BlobDirectory
    {
        internal BlobDirectory(string path)
        {
            Path = path.StartsWith("/") ? path : "/" + path;
        }

        public string Path
        {
            get; private set;
        }
    }

    public class BlobStorage
    {
        private readonly BlobContainerClient Container;

        public BlobStorage(string connectionString, string containerName)
        {
            Container = new BlobContainerClient(connectionString, containerName);
        }

        public async Task<Blob?> GetBlobAsync(string path)
        {
            return await ListBlobAsync(FixPath(path));
        }

        public async Task UploadBlobAsync(string path, Stream stream)
        {
            var existing = await GetBlobAsync(path);
            if (existing != null)
                await DeleteBlobAsync(path);

            var uploadResponse = await Container.UploadBlobAsync(FixPath(path), stream);
            if (uploadResponse == null)
                throw new InvalidOperationException($"Could not upload blob to {path}");

            var blob = Container.GetBlobClient(FixPath(path));
            var properties = await blob.GetPropertiesAsync();
            if (properties == null)
                return;

            var headers = new BlobHttpHeaders
            {
                ContentType = MimeTypes.GetMimeType(path),
                CacheControl = "max-age=600",

                // Populate remaining headers with the pre-existing properties
                ContentLanguage = properties.Value.ContentLanguage,
                ContentDisposition = properties.Value.ContentDisposition,
                ContentEncoding = properties.Value.ContentEncoding,
                ContentHash = properties.Value.ContentHash
            };

            await blob.SetHttpHeadersAsync(headers);
        }

        public async Task MoveBlobAsync(string oldPath, string newPath, bool replace = false)
        {
            var oldBlob = Container.GetBlobClient(FixPath(oldPath));
            var newBlob = Container.GetBlobClient(FixPath(newPath));

            if (!replace && !await oldBlob.ExistsAsync())
                return;

            if (!replace && await newBlob.ExistsAsync())
                throw new InvalidOperationException("The specified blob already exists");

            await newBlob.StartCopyFromUriAsync(oldBlob.Uri);
            await oldBlob.DeleteIfExistsAsync();
        }

        public async Task CopyBlobAsync(string oldPath, string newPath, bool replace = false)
        {
            var oldBlob = Container.GetBlobClient(FixPath(oldPath));
            var newBlob = Container.GetBlobClient(FixPath(newPath));

            if (!replace && !await oldBlob.ExistsAsync())
                return;

            if (!replace && await newBlob.ExistsAsync())
                throw new InvalidOperationException("The specified blob already exists");

            await newBlob.StartCopyFromUriAsync(oldBlob.Uri);
        }

        public async Task DeleteBlobAsync(string path)
        {
            var blob = Container.GetBlobClient(FixPath(path));
            await blob.DeleteIfExistsAsync();
        }

        public async Task<List<Blob>> ListBlobsAsync(string path, bool flat = false, int skip = 0, int take = -1)
        {
            if (flat)
                return await ListBlobsFlatListingAsync(FixPath(path), skip, take);
            return await ListBlobsHierarchicalListingAsync(FixPath(path), skip, take);
        }

        public async Task<List<BlobDirectory>> ListDirectoriesAsync(string path)
        {
            return await ListPrefixesHierarchicalListingAsync(FixPath(path), 0, -1);
        }

        string FixPath(string path)
        {
            path = (path ?? String.Empty).Replace('\\', '/').Trim(' ', '/');

            while (path.Contains("//"))
                path = path.Replace("//", "/");

            if (path.Length > 800)
                throw new InvalidOperationException("Path cannot exceed 800 characters");

            if (path.Contains(".."))
                throw new InvalidOperationException("Path cannot contain double ellipses '..'");

            if (path.Contains("//"))
                throw new InvalidOperationException("Path cannot contain double forward slash '//'");

            if (path.EndsWith("/"))
                throw new InvalidOperationException("Path cannot end with forward slash '/'");

            if (path.EndsWith("."))
                throw new InvalidOperationException("Path cannot end with dot '.'");

            return path;
        }

        // Implementation

        async Task<Blob?> ListBlobAsync(string path)
        {
            // Call the listing operation and return pages of the specified size.
            var resultSegment = Container.GetBlobsByHierarchyAsync(prefix: path, delimiter: "/").AsPages(default, 100);

            // Enumerate the blobs returned for each page.
            await foreach (Azure.Page<BlobHierarchyItem> blobPage in resultSegment)
            {
                // A hierarchical listing may return both virtual directories and blobs.
                foreach (BlobHierarchyItem blobhierarchyItem in blobPage.Values)
                {
                    if (!blobhierarchyItem.IsPrefix)
                    {
                        if (blobhierarchyItem.Blob.Name.EndsWith(path))
                            return new Blob(Container, blobhierarchyItem.Blob);
                    }
                }
            }

            return null;
        }

        async Task<List<Blob>> ListBlobsFlatListingAsync(string path, int skip = 0, int take = -1)
        {
            var result = new List<Blob>();

            // Call the listing operation and return pages of the specified size.
            var resultSegment = Container.GetBlobsAsync(prefix: path).AsPages(default, 100);

            // Enumerate the blobs returned for each page.
            await foreach (Azure.Page<BlobItem> blobPage in resultSegment)
            {
                if (take == 0)
                    break;

                foreach (BlobItem blobItem in blobPage.Values)
                {
                    skip--;
                    if (skip > 0)
                        continue;

                    if (take == 0)
                        break;
                    take--;

                    result.Add(new Blob(Container, blobItem));
                }
            }

            return result;
        }

        async Task<List<Blob>> ListBlobsHierarchicalListingAsync(string path, int skip = 0, int take = -1)
        {
            var result = new List<Blob>();

            // Call the listing operation and return pages of the specified size.
            var resultSegment = Container.GetBlobsByHierarchyAsync(prefix: path + "/", delimiter: "/").AsPages(default, 100);

            // Enumerate the blobs returned for each page.
            await foreach (Azure.Page<BlobHierarchyItem> blobPage in resultSegment)
            {
                // A hierarchical listing may return both virtual directories and blobs.
                foreach (BlobHierarchyItem blobhierarchyItem in blobPage.Values)
                {
                    if (take == 0)
                        break;

                    if (!blobhierarchyItem.IsPrefix)
                    {
                        skip--;
                        if (skip > 0)
                            continue;

                        if (take == 0)
                            break;
                        take--;

                        result.Add(new Blob(Container, blobhierarchyItem.Blob));
                    }
                }
            }

            return result;
        }

        async Task<List<BlobDirectory>> ListPrefixesHierarchicalListingAsync(string path, int skip = 0, int take = -1)
        {
            var result = new List<BlobDirectory>();

            // Call the listing operation and return pages of the specified size.
            var resultSegment = Container.GetBlobsByHierarchyAsync(prefix: path + "/", delimiter: "/").AsPages(default, 100);

            // Enumerate the blobs returned for each page.
            await foreach (Azure.Page<BlobHierarchyItem> blobPage in resultSegment)
            {
                // A hierarchical listing may return both virtual directories and blobs.
                foreach (BlobHierarchyItem blobhierarchyItem in blobPage.Values)
                {
                    if (take == 0)
                        break;

                    if (blobhierarchyItem.IsPrefix)
                    {
                        skip--;
                        if (skip > 0)
                            continue;

                        if (take == 0)
                            break;
                        take--;

                        result.Add(new BlobDirectory(blobhierarchyItem.Prefix.TrimEnd('/')));
                    }
                }
            }

            return result;
        }
    }
}
