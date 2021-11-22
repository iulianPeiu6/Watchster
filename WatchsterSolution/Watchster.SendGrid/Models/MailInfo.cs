using SendGrid.Helpers.Mail;

namespace Watchster.SendGrid.Models
{
    public class MailInfo
    {
        public EmailAddress Sender { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public EmailAddress Receiver { get; set; }
    }
}
