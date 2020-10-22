using System.Collections.Generic;

namespace Business.ValidationRules.FluentValidation.ValidationErrorObjects
{
    public class ValidationResult
    {
        public List<ValidationError> ValidationErrors { get; set; } = new List<ValidationError>();
    }
}