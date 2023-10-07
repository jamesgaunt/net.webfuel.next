using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel
{
    public class NotAuthenticatedException: ApplicationException
    {
        public override ProblemDetails ToProblemDetails()
        {
            // To the client this looks not different to Not Authorized
            return new ProblemDetails
            {
                Type = "/not-authorized",
                Title = "Not Authorised",
                Status = (int)HttpStatusCode.Unauthorized,
            };
        }
    }
}
