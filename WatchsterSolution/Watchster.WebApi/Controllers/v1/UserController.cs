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
        public IActionResult Authenticate([FromBody] UserAuthenticationDetails user)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult Register([FromBody] User user)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public IActionResult Delete(Guid userId)
        {
            throw new NotImplementedException();
        }

        [HttpPatch]
        public IActionResult Update(Guid userId, [FromBody] User user) 
        {
            throw new NotImplementedException();
        }
    }
}
