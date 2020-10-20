using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation.ValidationObjects
{
    public class ValidationResult
    {
        public List<ValidationError> ValidationErrors { get; set; } = new List<ValidationError>();
    }
}
