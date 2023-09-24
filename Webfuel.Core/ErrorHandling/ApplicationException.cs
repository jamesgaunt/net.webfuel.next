using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel
{
    public abstract class ApplicationException: Exception
    {
        public abstract ProblemDetails ToProblemDetails();
    }
}
