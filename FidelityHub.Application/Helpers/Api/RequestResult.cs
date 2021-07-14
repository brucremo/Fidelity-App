using System;
using System.Net;

namespace FidelityHub.Application.Helpers.Api
{
    public class RequestResult
    {
        public string Message { get; set; }
        public HttpStatusCode Code { get; set; }
        public DateTime Timestamp { get; set; }
        public object Data { get; set; }

        public RequestResult(HttpStatusCode code, string message, object model)
        {
            this.Code = code;
            this.Message = message;
            this.Timestamp = DateTime.Now;
            this.Data = model;
        }
    }
}
