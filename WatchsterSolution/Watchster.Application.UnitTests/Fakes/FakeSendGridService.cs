using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Watchster.SendGrid.Models;
using Watchster.SendGrid.Services;

namespace Watchster.Application.UnitTests.Fakes
{
    public class FakeSendGridService : ISendGridService
    {
        public Task SendMailAsync(MailInfo mail)
        {
            string text;
            var email = new MailAddress(mail.Receiver.Email);
            if (!EmailIsValid(mail.Receiver.Email)) 
                throw new AggregateException();
            return new Task(() => text = mail.Receiver.Email);
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
