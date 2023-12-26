using System.Net;

namespace Microsoft.AspNetCore.Mvc
{
    public class HEFHttpResult
    {
        public int Code { get; set; }

        public string Msg { get; set; }

        public bool Success => Code == (int)HttpStatusCode.OK;
    }

    public class HEFHttpResult<TResultData> : HEFHttpResult
    {
        public TResultData Data { get; set; }
    }
}
