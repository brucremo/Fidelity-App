using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FidelityHub.Application.Interfaces
{
    public interface ISendGridConnector
    {
        Task<HttpStatusCode> SendContactRequest(string emailAddress);
    }
}
