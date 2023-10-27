using Azure.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain
{
    public class LoginUser : IRequest<StringResult>
    {
        public required string Email { get; set; }

        public required string Password { get; set; }
    }
}
