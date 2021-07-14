using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FidelityHub.Application.Helpers.Api;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FidelityHub.Web.Controllers
{
    [Authorize(Policy = "AuthenticatedUser")]
    public class SharedController : BaseController
    {
        public SharedController(IMediator mediator) : base(mediator)
        {

        }
    }
}
