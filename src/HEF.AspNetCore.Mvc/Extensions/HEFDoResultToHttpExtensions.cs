using HEF.Core;
using System;
using System.Net;

namespace Microsoft.AspNetCore.Mvc
{
    public static class HEFDoResultToHttpExtensions
    {
        public static HEFHttpResult ToHttpResult(this HEFDoResult doResult)
        {
            if (doResult == null)
                throw new ArgumentNullException(nameof(doResult));

            return new HEFHttpResult
            {
                Code = DoResultTypeToHttpCode(doResult.Type),
                Msg = doResult.Msg
            };
        }

        public static HEFHttpResult<TResultData> ToHttpResult<TResultData>(this HEFDoResult<TResultData> doResult)
        {
            if (doResult == null)
                throw new ArgumentNullException(nameof(doResult));

            return new HEFHttpResult<TResultData>
            {
                Code = DoResultTypeToHttpCode(doResult.Type),
                Msg = doResult.Msg,
                Data = doResult.Data
            };
        }

        private static int DoResultTypeToHttpCode(string doResultTypeStr)
        {
            if (string.IsNullOrWhiteSpace(doResultTypeStr))
                return 0;

            if (Enum.TryParse<HEFDoResultType>(doResultTypeStr, true, out var doResultType))
            {
                return doResultType switch
                {
                    HEFDoResultType.success => (int)HttpStatusCode.OK,
                    HEFDoResultType.fail => (int)HttpStatusCode.InternalServerError,
                    HEFDoResultType.validFail => (int)HttpStatusCode.BadRequest,
                    HEFDoResultType.notFound => (int)HttpStatusCode.NotFound,
                    _ => 0
                };
            }

            return 0;
        }
    }
}
