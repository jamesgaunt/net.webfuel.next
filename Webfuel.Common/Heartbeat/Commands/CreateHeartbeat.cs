using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Common
{
    public class CreateHeartbeat: IRequest<Heartbeat>
    {
        public required string Name { get; init; }
    }
}
