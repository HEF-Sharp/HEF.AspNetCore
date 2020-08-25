using HEF.Core;

namespace Microsoft.AspNetCore.Mvc.ModelBinding
{
    public interface IModelStateDoResultFactory
    {
        HEFDoResult CreateValidationDoResult(ModelStateDictionary modelStateDictionary);
    }
}
