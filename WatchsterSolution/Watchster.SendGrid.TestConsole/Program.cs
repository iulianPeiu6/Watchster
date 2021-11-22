using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Mail;
using Watchster.SendGrid.Models;
using Watchster.SendGrid.Services;

namespace Watchster.SendGrid.TestConsole
{
    static class Program
    {
        static void Main()
        {
            var services = new ServiceCollection()
                .AddSendGrid()
                .AddLogging(configure => configure.AddConsole())
                .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Information)
                .BuildServiceProvider();

            var sendGridService = services.GetRequiredService<ISendGridService>();

            TestSendMail(sendGridService);
        }

        private static void TestSendMail(ISendGridService sendGridService)
        {
            var mail = new MailInfo
            {
                Subject = "Mail Subject Test",
                Body = "Mail Body Test",
                Receiver = new EmailAddress()
                {
                    Name = "Alex",
                    Email = "watchster.integration@gmail.com"
                }
            };
            sendGridService.SendMailAsync(mail).Wait();
        }
    }
}
