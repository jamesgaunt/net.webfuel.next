﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel
{
    [ApiIgnore]
    public class IdentityUser
    {
        public required Guid Id { get; init; }

        public required string Email { get; init; }
    }
}
