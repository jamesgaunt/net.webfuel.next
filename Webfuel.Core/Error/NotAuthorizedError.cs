using FluentValidation;
using FluentValidation.Results;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel
{
    [ApiType]
    public class NotAuthorizedError: Error
    {
        public NotAuthorizedError()
        {
        }

        public NotAuthorizedError(UnauthorizedAccessException unauthorizedAccessException)
        {
            Message = unauthorizedAccessException.Message;
        }

        public override string ErrorType => "Not Authorized Error";
    }
}
