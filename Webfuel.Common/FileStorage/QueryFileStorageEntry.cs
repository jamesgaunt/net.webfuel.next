using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Common
{
    public class QueryFileStorageEntry: Query
    {
        public Guid FileStorageGroupId { get; set; }

        public Query ApplyCustomFilters()
        {
            this.Equal(nameof(FileStorageEntry.FileStorageGroupId), FileStorageGroupId);
            this.Contains(nameof(FileStorageEntry.FileName), Search);
            return this;
        }
    }
}
