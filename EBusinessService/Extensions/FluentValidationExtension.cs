using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EBusinessService.Extensions
{
    public static class FluentValidationExtension
    {
        public static void AddToModelStatte(this ValidationResult result, ModelStateDictionary modelState)
        {
            foreach (var error in result.Errors)
            {
                modelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
        }
    }
}
