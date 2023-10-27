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
    public class ChangeUserPassword : IRequest
    {
        public required string CurrentPassword { get; set; }

        public required string NewPassword { get; set;}

        public required string ConfirmNewPassword { get; set;}
    }
}
