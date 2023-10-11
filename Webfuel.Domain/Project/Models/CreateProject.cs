using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain
{
    public class CreateProject: IRequest<Project>
    {
        public required string Title { get; set; }
    }
}
