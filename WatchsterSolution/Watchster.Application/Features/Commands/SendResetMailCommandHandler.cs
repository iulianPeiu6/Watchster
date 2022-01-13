using MediatR;
using SendGrid.Helpers.Mail;
using System;
using System.Threading;
using System.Threading.Tasks;
using Watchster.Application.Models;
using Watchster.SendGrid.Models;
using Watchster.SendGrid.Services;


namespace Watchster.Application.Features.Commands
{
    public class SendResetMailCommandHandler : IRequestHandler<SendResetMailCommand, string>
    {
        private readonly ISendGridService sendGridService;

        public SendResetMailCommandHandler(ISendGridService sendGridService)
        {
            this.sendGridService = sendGridService;
        }
        public async Task<string> Handle(SendResetMailCommand request, CancellationToken cancellationToken)
        {
            var mail = new MailInfo
            {
                Subject = "Watchster : Reset Password",
                Body = $"Hello, <br/><br/>" +
                    $"You have requested to change your password. <br/>" +
                    $"Click <a href='{request.Endpoint}/{request.Result.Code}'>here</a> to reset your password.<br/>" +
                    $"Watchster, <br/>" +
                    $"Your Online Movie Recommender",
                Receiver = new EmailAddress()
                {
                    Email = request.Result.Email
                }
            };

            try
            {
                await sendGridService.SendMailAsync(mail);
            }
            catch (Exception)
            {
                return Error.EmailNotSent;
            }
            return "Email was sent";
        }
    }

}
