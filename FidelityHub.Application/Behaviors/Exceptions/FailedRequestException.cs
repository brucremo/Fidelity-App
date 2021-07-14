using System;
using System.Collections.Generic;
using System.Text;

namespace FidelityHub.Application.Behaviors.Exceptions
{
    public class FailedRequestException : Exception
    {
        public FailedRequestException(string message) : base(message)
        {
        }
    }
}
