using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel
{
    [ApiStatic]
    public enum ErrorType
    {
        UnknownError,
        ValidationError,
        NotAuthorizedError,
        NotAuthenticatedError,
        DatabaseError,
    }

    [ApiType]
    public class Error
    {
        public required ErrorType ErrorType { get; set; }

        public string Message { get; set; } = String.Empty;

        public List<ValidationError> ValidationErrors { get; } = new List<ValidationError>();
    }

    [ApiType]

    public class ValidationError
    {
        public string Property { get; set; } = String.Empty;

        public string Message { get; set; } = String.Empty;
    }
}
