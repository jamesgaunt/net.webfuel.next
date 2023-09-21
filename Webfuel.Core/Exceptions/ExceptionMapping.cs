using FluentValidation;
using Microsoft.Data.SqlClient;

namespace Webfuel
{
    internal static class ExceptionMapping
    {

        public static ErrorResponse ToError(this SqlException exception)
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

            return new ErrorResponse { ErrorType = ErrorResonseType.DatabaseError, Message = message };
        }

        public static ErrorResponse ToError(this ValidationException exception)
        {
            var error = new ErrorResponse { ErrorType = ErrorResonseType.ValidationError, Message = exception.Message };

            foreach(var item in exception.Errors)
            {
                error.ValidationErrors.Add(new ValidationError { Property = item.PropertyName, Message = item.ErrorMessage.Replace("'", "") });
            }

            return error;
        }

        public static ErrorResponse ToError(this NotAuthorizedException exception)
        {
            return new ErrorResponse { ErrorType = ErrorResonseType.NotAuthorizedError, Message = "Not Authorized" };
        }

        public static ErrorResponse ToError(this NotAuthenticatedException exception)
        {
            return new ErrorResponse { ErrorType = ErrorResonseType.NotAuthenticatedError, Message = "Not Authenticated" };
        }
    }
}
