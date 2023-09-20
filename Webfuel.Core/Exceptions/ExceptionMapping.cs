using FluentValidation;
using Microsoft.Data.SqlClient;

namespace Webfuel
{
    internal static class ExceptionMapping
    {

        public static Error ToError(this SqlException exception)
        {
            var message = exception.Message;

            if (message.Contains("The DELETE statement conflicted with the REFERENCE constraint"))
            {
                message = "Cannot delete this item as it is being used by other items in the database";
            }
            else if (message.Contains("Cannot insert duplicate key row in object"))
            {
                message = "Cannot create this item as it would conflict with items already in the database";
            }

            return new Error { ErrorType = ErrorType.DatabaseError, Message = message };
        }

        public static Error ToError(this ValidationException exception)
        {
            var error = new Error { ErrorType = ErrorType.ValidationError, Message = exception.Message };

            foreach(var item in exception.Errors)
            {
                error.ValidationErrors.Add(new ValidationError { Property = item.PropertyName, Message = item.ErrorMessage });
            }

            return error;
        }

        public static Error ToError(this NotAuthorizedException exception)
        {
            return new Error { ErrorType = ErrorType.NotAuthorizedError, Message = "Not Authorized" };
        }

        public static Error ToError(this NotAuthenticatedException exception)
        {
            return new Error { ErrorType = ErrorType.NotAuthenticatedError, Message = "Not Authenticated" };
        }
    }
}
