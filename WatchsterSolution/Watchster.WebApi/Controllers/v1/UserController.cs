using EzPasswordValidator.Checks;
using EzPasswordValidator.Validators;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Mail;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Watchster.Application.Features.Commands;
using System.Threading.Tasks;
using Watchster.Domain.Entities;
using Watchster.Jwt.Services;
using Watchster.Jwt;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;

namespace Watchster.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class UserController : BaseController
    {
        IMediator mediator;
        public UserController(IMediator mediator) : base(mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] CreateUserCommand command)
        {
            IActionResult result = BadRequest("The password does not respect the constraints or the email address is invalid.");
            try
            {
                if (EmailIsValid(command.Email) && PasswordRespectsContraints(command.Password))
                {
                    //Add user to database and obtain his guid
                    var userGuid = await mediator.Send(command);

                    var user = new User
                    {
                        Email = command.Email,
                        Password = command.Password,
                        Id = userGuid,
                        IsSubscribed = command.IsSubscribed,
                    };

                    //Obtain user Jwt token
                }
            }
            catch(Exception) { }
            return result;
        }

        private static bool PasswordRespectsContraints(string password)
        {

            var validator = new PasswordValidator(CheckTypes.Letters | CheckTypes.Numbers | CheckTypes.Length | CheckTypes.Symbols);
            validator.SetLengthBounds(8, 16);
            return validator.Validate(password);
        }

        private static bool EmailIsValid(string email)
        {
            try
            {
                var emailAddress = new MailAddress(email);
                return emailAddress.Address == email;
            }
            catch
            {
                return false;
            }
        }
        /*

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
        */
    }
}
