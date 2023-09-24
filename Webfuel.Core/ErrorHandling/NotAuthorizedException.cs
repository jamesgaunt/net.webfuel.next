using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel
{
    public class NotAuthorizedException: ApplicationException
    {
        public override ProblemDetails ToProblemDetails()
        {
            return new ProblemDetails
            {
                Type = "/not-authorized",
                Title = "Not Authorised",
                Status = (int)HttpStatusCode.Unauthorized,
            };
        }
    }
}
