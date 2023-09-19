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
    public class ValidationError: Error
    {
        public ValidationError()
        {
        }

        public ValidationError(ValidationException validationException)
        {
            Message = validationException.Message;

            foreach(var error in validationException.Errors)
            {
                Errors.Add(new ValidationErrorProperty(error));
            }
        }

        public List<ValidationErrorProperty> Errors { get; } = new List<ValidationErrorProperty>();

        public override string ErrorType => "Validation Error";
    }

    [ApiType]

    public class ValidationErrorProperty
    {
        public ValidationErrorProperty(string propertyName, string errorMessage)
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
        }

        public ValidationErrorProperty(ValidationFailure failure)
        {
            PropertyName = failure.PropertyName;
            ErrorMessage = failure.ErrorMessage;
        }

        public string PropertyName { get; }

        public string ErrorMessage { get; }
    }
}
