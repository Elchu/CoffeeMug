using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeMug.Services
{
    public class ErrorMessageManager
    {
        public static string ShowErrors(ModelStateDictionary modelState)
        {
            var errors = string.Join(Environment.NewLine, modelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));

            return errors;
        }
    }
}
