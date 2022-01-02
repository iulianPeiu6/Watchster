using MediatR;
using SendGrid.Helpers.Mail;
using System;
using System.Threading;
using System.Threading.Tasks;
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
                Subject = "Password reset Watchster Application",
                Body = "Password reset link: " + request.Endpoint + "/" + request.Result.Code,
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
                return "Error at sending email";
            }
            return "Email was sent";
        }
    }

}
