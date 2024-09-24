using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain;

public class ProjectSupportFile
{
    public required Guid Id { get; set; }

    public required string FileName { get; set; }

    public required Int64 SizeBytes { get; set; }

    public DateTimeOffset? UploadedAt { get; set; }
}
