using FakeItEasy;
using Faker;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Watchster.Application.Authentication.Models;
using Watchster.Application.Features.Commands;
using Watchster.Application.Features.Queries;
using Watchster.Application.Models;
using Watchster.Domain.Entities;
using Watchster.WebApi.Controllers.v1;

namespace Watchster.WebApi.UnitTests.v1.Abstracts
{
    public class UserControllerTestsBase
    {
        protected readonly UserController controller;
        protected readonly IMediator mediator;
        protected readonly ILogger<UserController> logger;
        protected const string InvalidEmailAddress = "invalid email";
        protected const string UnreachableEmailAddress = "unreachable email";
        protected const string ValidPasswordCode = "ValidCode69";
        protected const string InvalidPasswordCode = "InvalidCode96";

        public UserControllerTestsBase()
        {
            var fakeMediator = new Fake<IMediator>();

            fakeMediator.CallsTo(mediator => mediator.Send(A<GetUserDetailsQuery>.That.Matches(u => u.UserId < 0), default))
                .Throws<ArgumentException>();

            fakeMediator.CallsTo(mediator => mediator.Send(A<CreateUserCommand>.That.Matches(u => u.Email == InvalidEmailAddress), default))
                .Throws<ArgumentException>();

            fakeMediator.CallsTo(mediator => mediator.Send(A<AuthenticateUserCommand>.That.Matches(u => u.Email == InvalidEmailAddress), default))
                .Returns(Task.FromResult(
                    new UserAuthenticationResult
                    {
                        ErrorMessage = Error.WrongEmailOrPass
                    }));

            fakeMediator.CallsTo(mediator => mediator.Send(A<GenerateAndSaveResetPasswordCodeCommand>.That.Matches(u => u.Email == InvalidEmailAddress), default))
                .Returns(Task.FromResult(
                    new ResetPasswordCodeResult
                    {
                        ErrorMessage = Error.EmailNotFound
                    }));

            fakeMediator.CallsTo(mediator => mediator.Send(A<GenerateAndSaveResetPasswordCodeCommand>.That.Matches(u => u.Email == UnreachableEmailAddress), default))
                .Returns(Task.FromResult(
                    new ResetPasswordCodeResult
                    {
                        ResetPasswordCode = new ResetPasswordCode()
                        {
                            Email = UnreachableEmailAddress,
                        }
                    }));

            fakeMediator.CallsTo(mediator => mediator.Send(A<GenerateAndSaveResetPasswordCodeCommand>.That.Matches(u => u.Email != UnreachableEmailAddress && u.Email != InvalidEmailAddress), default))
                .Returns(Task.FromResult(
                    new ResetPasswordCodeResult
                    {
                        ResetPasswordCode = new ResetPasswordCode()
                        {
                            Email = Internet.Email(),
                        }
                    }));

            fakeMediator.CallsTo(mediator => mediator.Send(A<SendResetMailCommand>.That.Matches(u => u.Result.Email == UnreachableEmailAddress), default))
                .Returns(Task.FromResult(Error.EmailNotSent));

            fakeMediator.CallsTo(mediator => mediator.Send(A<VerifyPasswordCodeCommand>.That.Matches(c => c.Code == ValidPasswordCode), default))
                .Returns(Task.FromResult(true));

            fakeMediator.CallsTo(mediator => mediator.Send(A<VerifyPasswordCodeCommand>.That.Matches(c => c.Code == InvalidPasswordCode), default))
                .Returns(Task.FromResult(false));

            fakeMediator.CallsTo(mediator => mediator.Send(A<ChangeUserPasswordCommand>.That.Matches(c => c.Code == ValidPasswordCode), default))
                .Returns(Task.FromResult(new ChangePasswordResult
                {
                    IsSuccess = true,
                }));

            fakeMediator.CallsTo(mediator => mediator.Send(A<ChangeUserPasswordCommand>.That.Matches(c => c.Code == InvalidPasswordCode), default))
                .Returns(Task.FromResult(new ChangePasswordResult
                {
                    IsSuccess = false,
                }));

            mediator = fakeMediator.FakedObject;
            logger = A.Fake<ILogger<UserController>>();

            controller = new UserController(mediator, logger);
        }
    }
}
