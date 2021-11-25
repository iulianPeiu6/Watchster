using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using Watchster.WebApi.Models;

namespace Watchster.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class UserController : BaseController
    {
        public UserController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register([FromBody] User user)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("Authenticate")]
        public IActionResult Authenticate([FromBody] UserAuthenticationDetails user)
        {
            throw new NotImplementedException();
        }

        [HttpPatch]
        [Route("Update")]
        public IActionResult Update(Guid userId, [FromBody] User user)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
