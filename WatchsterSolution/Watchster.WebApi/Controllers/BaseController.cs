using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Watchster.WebApi.Controllers
{
    [Route("api/{version:apiVersion}/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IMediator mediator;

        public BaseController(IMediator mediator)
        {
            this.mediator = mediator;
        }
    }
}
