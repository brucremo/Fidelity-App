using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FidelityHub.Application.Helpers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public IMediator Mediator;

        public BaseController(IMediator mediator)
        {
            this.Mediator = mediator;
        }
    }
}
