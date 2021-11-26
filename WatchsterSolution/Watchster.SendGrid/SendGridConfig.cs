using SendGrid.Helpers.Mail;

namespace Watchster.SendGrid
{
    public class SendGridConfig
    {
        public EmailAddress Sender { get; set; }

        public string ApiKey { get; set; }
    }
}
