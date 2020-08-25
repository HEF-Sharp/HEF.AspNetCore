using HEF.Core;
using HEF.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Microsoft.AspNetCore.Mvc.ModelBinding
{
    internal class ModelStateDoResultFactory : IModelStateDoResultFactory
    {
        private const string ModelError_DefaultMessage = "The value is invalid";

        public HEFDoResult CreateValidationDoResult(ModelStateDictionary modelStateDictionary)
        {
            if (modelStateDictionary == null)
                throw new ArgumentNullException(nameof(modelStateDictionary));

            var validateResults = FetchValidationErrors(modelStateDictionary);

            return HEFDoResultHelper.DoValidate(validateResults.ToArray());
        }

        private static IEnumerable<ValidationResult> FetchValidationErrors(ModelStateDictionary modelState)
        {
            foreach (var keyModelStatePair in modelState)
            {
                var key = keyModelStatePair.Key;
                var errors = keyModelStatePair.Value.Errors;
                if (errors.IsNotEmpty())
                {
                    var errorMessages = errors.Select(error =>
                    {
                        return error.ErrorMessage.IsNullOrWhiteSpace() ?
                            ModelError_DefaultMessage : error.ErrorMessage;
                    });
                    var errorMessageStr = string.Join(Environment.NewLine, errorMessages);

                    yield return new ValidationResult(errorMessageStr, new[] { key });
                }
            }
        }
    }
}
