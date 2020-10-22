using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.ValidationRules.FluentValidation.ValidationErrorObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAPI.ActionAttributes
{
    public class ValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errorsInModelState = context.ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage)).ToArray();

                ValidationResult validationResult = new ValidationResult();

                foreach (var error in errorsInModelState)
                {
                    foreach (var subError in error.Value)
                    {
                        ValidationError errorModel = new ValidationError
                        {
                            FieldName = error.Key,
                            ValidationMessage = subError
                        };

                        validationResult.ValidationErrors.Add(errorModel);
                    }
                }

                context.Result = new BadRequestObjectResult(validationResult);
            }
        }
    }
}
