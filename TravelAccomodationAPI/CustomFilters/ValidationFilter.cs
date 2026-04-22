using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TravelAccomodationAPI.CustomFilters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        private readonly IServiceProvider _serviceProvider;

        public ValidationFilter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            foreach (var argument in context.ActionArguments.Values)
            {
                if (argument == null) continue;

                var validatorType = typeof(IValidator<>).MakeGenericType(argument.GetType());
                var validator = _serviceProvider.GetService(validatorType);

                if (validator == null) continue;

                var validationContext = new ValidationContext<object>(argument);

                var result = await ((IValidator)validator).ValidateAsync(validationContext);

                if (!result.IsValid)
                {
                    // FORMAT RESPONSE HERE
                    var errors = result.Errors
                        .GroupBy(e => char.ToLowerInvariant(e.PropertyName[0]) + e.PropertyName.Substring(1))
                        .ToDictionary(
                            g => g.Key,
                            g => g.Select(e => e.ErrorMessage).Distinct().ToArray()
                        );

                    context.Result = new BadRequestObjectResult(errors);
                    return;
                }
            }

            await next();
        }

    }
}