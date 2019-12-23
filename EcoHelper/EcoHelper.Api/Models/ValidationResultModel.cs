namespace EcoHelper.Api.Models
{
    using FluentValidation;
    using System.Collections.Generic;
    using System.Linq;

    public class ValidationResultModel
    {
        public ValidationResultModel(ValidationException validationException)
        {
            Message = validationException.Message.Equals(string.Empty) ? "Validation Failed" : validationException.Message;
            Errors = validationException.Errors.Select(e => new ValidationError(e.PropertyName, e.ErrorMessage)).ToList();
        }

        public List<ValidationError> Errors { get; }

        public string Message { get; }
    }
}
