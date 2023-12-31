﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain
{
    public class UpdateProjectStatus: IRequest<Project>
    {
        public required Guid Id { get; set; }
        public required Guid StatusId { get; set; }
    }
}
