using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FidelityHub.Application.Helpers.Api;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FidelityHub.Web.Controllers
{
    public class RewardEngineController : BaseController
    {
        public RewardEngineController(IMediator mediator) : base(mediator)
        {
        }
    }
}
