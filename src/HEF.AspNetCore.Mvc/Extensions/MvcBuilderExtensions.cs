using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MvcBuilderExtensions
    {
        public static IMvcBuilder AddInvalidModelStateDoResultResponse(this IMvcBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            builder.Services.AddSingleton<IModelStateDoResultFactory, ModelStateDoResultFactory>();
            builder.Services.TryAddEnumerable(
                ServiceDescriptor.Transient<IConfigureOptions<ApiBehaviorOptions>, ApiBehaviorOptionsDoResultResponseSetup>());

            return builder;
        }
    }
}
