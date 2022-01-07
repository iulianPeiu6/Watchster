using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Watchster.Application.Features.Commands;
using Watchster.Application.Features.Queries;
using Watchster.Application.Models;

namespace Watchster.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class UserController : BaseController
    {
        private readonly ILogger<UserController> logger;

        public UserController(IMediator mediator, ILogger<UserController> logger) : base(mediator)
        {
            this.logger = logger;
        }

        [HttpGet]
        [Route("GetUser")]
        public async Task<IActionResult> GetUserAsync(int userId)
        {
            logger.LogInformation("Handeling request on User/GetUser/{userId}");

            try
            {
                var query = new GetUserDetailsQuery
                {
                    UserId = userId
                };
                var response = await mediator.Send(query);
                return Ok(response);
            }
            catch (ArgumentException)
            {
                return NotFound(new { Message = Error.UserNotFound });
            }
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] CreateUserCommand command)
        {
            logger.LogInformation("Handeling request on User/Register");

            try
            {
                var response = await mediator.Send(command);

                return Ok(response);
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("Authenticate")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] AuthenticateUserCommand command)
        {
            logger.LogInformation("Handeling request on User/Authenticate");

            var response = await mediator.Send(command);

            if (response.ErrorMessage == Error.WrongEmailOrPass)
            {
                return Unauthorized(response);
            }

            return Ok(response);
        }

        [HttpPatch]
        [Route("SendEmailChangePassword")]
        public async Task<IActionResult> SendEmailChangePasswordAsync([FromBody] GenerateAndSaveResetPasswordCodeCommand command)
        {
            logger.LogInformation("Handeling request on User/SendEmailChangePassword");

            var responseSaveResetPasswordCode = await mediator.Send(command);

            if (responseSaveResetPasswordCode.ErrorMessage == Error.EmailNotFound)
            {
                return NotFound(new { Message = responseSaveResetPasswordCode.ErrorMessage });
            }

            var commandSendMail = new SendResetMailCommand
            {
                Result = responseSaveResetPasswordCode.ResetPasswordCode,
                Endpoint = command.Endpoint
            };

            var responseSendMail = await mediator.Send(commandSendMail);

            if (responseSendMail == Error.EmailNotSent)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, responseSendMail);
            }

            return Ok(new { Message = responseSendMail });
        }

        [HttpPost]
        [Route("VerifyPasswordCode")]
        public async Task<IActionResult> VerifyPasswordCodeAsync([FromBody] VerifyPasswordCodeCommand command)
        {
            logger.LogInformation("Handeling request on User/VerifyPasswordCode");

            var codeIsValid = await mediator.Send(command);

            if (!codeIsValid)
            {
                return Unauthorized(new { Message = Error.WrongPassChangeCode });
            }

            return Ok(new { Message = "Valid code" });
        }

        [HttpPatch]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangeUserPasswordCommand command)
        {
            logger.LogInformation("Handeling request on User/ChangeNewPassword");

            var response = await mediator.Send(command);

            if (!response.IsSuccess)
            {
                return Unauthorized(new { Message = response.ErrorMessage });
            }

            return Ok(new { Message = "Password changed!" });
        }
    }
}
