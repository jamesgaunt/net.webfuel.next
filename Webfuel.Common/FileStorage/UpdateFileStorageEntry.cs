namespace Webfuel.Common;

[ApiType]
public class UpdateFileStorageEntry
{
    public Guid Id { get; set; }

    public List<Guid> FileTagIds { get; set; } = new List<Guid>();
}
