namespace Webfuel
{
    [ApiType]
    public class ValidationProblemDetails: ProblemDetails
    {
        public List<ValidationError> ValidationErrors { get; } = new List<ValidationError>();

        public class ValidationError
        {
            public string Property { get; set; } = String.Empty;

            public string Message { get; set; } = String.Empty;
        }

        public void AddValidationError(string property, string message)
        {
            ValidationErrors.Add(new ValidationError {  Property = property, Message = message });
        }
    }
}
