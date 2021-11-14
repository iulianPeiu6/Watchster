using System.Threading.Tasks;
using Watchster.SendGrid.Models;

namespace Watchster.SendGrid.Services
{
    public interface ISendGridService
    {
        Task SendMailAsync(MailInfo mail);
    }
}