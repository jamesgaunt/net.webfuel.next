using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Webfuel.Common
{
    [ApiType]
    public class UploadFileStorageEntry
    {
        public Guid FileStorageGroupId { get; set; }

        [JsonIgnore]
        public IFormFile? FormFile { get; set; }
    }
}
