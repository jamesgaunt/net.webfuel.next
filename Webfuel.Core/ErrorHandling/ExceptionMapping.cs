using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Net;

namespace Webfuel
{
    internal static class ExceptionMapping
    {
        public static ProblemDetails ToProblemDetails(this SqlException exception)
        {
            var title = exception.Message;

            if (title.Contains("The DELETE statement conflicted with the REFERENCE constraint"))
            {
                title = "Cannot delete this item as it is being used by other items in the database";
            }
            else if (title.Contains("Cannot insert duplicate key row in object"))
            {
                title = "Cannot create this item as it would conflict with items already in the database";
            }

            return new ProblemDetails
            {
                Type = "/invalid-operation",
                Title = title,
                Status = (int)HttpStatusCode.BadRequest,
            };
        }

        public static ValidationProblemDetails ToProblemDetails(this ValidationException exception)
        {
            var problemDetails = new ValidationProblemDetails
            {
                Type = "/validation-errors",
                Title = "Please review and correct validation errors",
                Status = (int)HttpStatusCode.BadRequest
            };

            foreach(var item in exception.Errors)
            {
                problemDetails.AddValidationError(item.PropertyName, item.ErrorMessage.Replace("'", ""));
            }

            return problemDetails;
        }

    }
}
