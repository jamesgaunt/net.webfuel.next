using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Common;

namespace Webfuel.Domain
{
    public class ListSupportRequestFiles: IRequest<List<FileStorageEntry>>
    {
        public required Guid SupportRequestId { get; set; }
    }
}
