using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel
{
    public class DomainException: ApplicationException
    {
        public DomainException(string message) :base(message) { }

        public override ProblemDetails ToProblemDetails()
        {
            // To the client this looks not different to Not Authorized
            return new ProblemDetails
            {
                Type = "/domain-error",
                Title = Message,
                Status = (int)HttpStatusCode.BadRequest,
            };
        }
    }
}
