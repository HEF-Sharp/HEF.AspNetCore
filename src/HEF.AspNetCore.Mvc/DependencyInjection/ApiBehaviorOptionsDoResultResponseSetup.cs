using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    internal class ApiBehaviorOptionsDoResultResponseSetup : IConfigureOptions<ApiBehaviorOptions>
    {
        public void Configure(ApiBehaviorOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            options.InvalidModelStateResponseFactory = context =>
            {
                var modelStateDoResultFactory = context.HttpContext.RequestServices.GetRequiredService<IModelStateDoResultFactory>();

                return CreateInvalidModelStateDoResultResponse(modelStateDoResultFactory, context);
            };
        }

        private static IActionResult CreateInvalidModelStateDoResultResponse(
            IModelStateDoResultFactory modelStateDoResultFactory, ActionContext context)
        {
            var validationDoResult = modelStateDoResultFactory.CreateValidationDoResult(context.ModelState);

            var result = new OkObjectResult(validationDoResult);

            result.ContentTypes.Add("application/json");
            result.ContentTypes.Add("application/xml");

            return result;
        }
    }
}
