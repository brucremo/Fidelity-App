using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace FidelityHub.Application.Helpers
{
    public static class ResponseHelpers
    {
        public static bool IsStatusCodeSuccessful(this HttpStatusCode statusCode)
        {
            return statusCode == HttpStatusCode.OK || statusCode == HttpStatusCode.Created || statusCode == HttpStatusCode.Accepted || statusCode == HttpStatusCode.Accepted;
        }
    }
}
