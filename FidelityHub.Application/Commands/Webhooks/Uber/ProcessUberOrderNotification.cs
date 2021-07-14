using MediatR;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace FidelityHub.Application.Commands.Webhooks.Uber
{
    public class ProcessUberOrderNotification : IRequest<HttpStatusCode>
    {
    }
}
