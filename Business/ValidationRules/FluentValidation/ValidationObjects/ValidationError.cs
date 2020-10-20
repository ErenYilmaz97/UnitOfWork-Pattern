using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation.ValidationObjects
{
    public class ValidationError
    {
        public string FieldName { get; set; }
        public string ValidationMessage { get; set; }
    }
}
