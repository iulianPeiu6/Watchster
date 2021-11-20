using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Net.Mail;
using System.Threading.Tasks;
using Vanguard;
using Watchster.SendGrid.Models;

namespace Watchster.SendGrid.Services
{
    public class SendGridService : ISendGridService
    {
        private readonly ILogger<SendGridService> logger;
        private readonly SendGridConfig config;

        public SendGridService(ILogger<SendGridService> logger, IOptions<SendGridConfig> config)
        {
            this.logger = logger;
            this.config = config.Value;
        }

        public async Task SendMailAsync(MailInfo mail)
        {
            try
            {
                ValidateMail(mail);

                logger.LogInformation($"Sending Email from '{mail.Sender.Email}' to '{mail.Receiver.Email}'...");

                var sendGridClient = new SendGridClient(config.ApiKey);
                var message = new SendGridMessage
                {
                    From = mail.Sender,
                    Subject = mail.Subject
                };
                message.AddContent(MimeType.Text, mail.Body);
                message.AddTo(mail.Receiver);

                var response = await sendGridClient.SendEmailAsync(message).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    logger.LogInformation($"Mail sent");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    throw new ArgumentException("Sender Key is invalid!");
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to send mail: {ex.Message}", ex);
                throw;
            }
        }

        private void ValidateMail(MailInfo mail)
        {
            Guard.ArgumentNotNull(mail, nameof(mail));
            Guard.ArgumentNotNullOrEmpty(mail.Receiver.Email, nameof(mail.Receiver.Email));
            
            if (mail.Sender is null)
            {
                mail.Sender = config.Sender;
            }

            if (!EmailIsValid(mail.Sender.Email))
            {
                throw new ArgumentException("Sender Email Address is Invalid");
            }

            if (!EmailIsValid(mail.Receiver.Email))
            {
                throw new ArgumentException("Receiver Email Address is Invalid");
            }
        }

        private bool EmailIsValid(string email)
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
    }
}
