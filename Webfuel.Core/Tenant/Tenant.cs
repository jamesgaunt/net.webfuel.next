using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel
{
    public class Tenant
    {
        public required string Name { get; init; }

        public required string DatabaseSchema { get; init; }

        public required string DatabasePassword { get; init; }
    }
}
