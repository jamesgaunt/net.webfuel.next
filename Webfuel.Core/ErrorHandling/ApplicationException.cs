using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel
{
    public abstract class ApplicationException: Exception
    {
        public ApplicationException() { }

        public ApplicationException(string message) :base(message) { }

        public abstract ProblemDetails ToProblemDetails();
    }
}
