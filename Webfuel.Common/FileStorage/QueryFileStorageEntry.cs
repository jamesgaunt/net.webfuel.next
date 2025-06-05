namespace Webfuel.Common;

public class QueryFileStorageEntry : Query
{
    public Guid FileStorageGroupId { get; set; }

    public Guid? FileTagId { get; set; }

    public Query ApplyCustomFilters()
    {
        this.Equal(nameof(FileStorageEntry.FileStorageGroupId), FileStorageGroupId);
        this.Contains(nameof(FileStorageEntry.FileTagIds), FileTagId?.ToString(), FileTagId.HasValue);
        this.Contains(nameof(FileStorageEntry.FileName), Search);
        return this;
    }
}
