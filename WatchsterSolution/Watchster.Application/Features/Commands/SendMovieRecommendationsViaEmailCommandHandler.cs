using MediatR;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Watchster.Application.Models;
using Watchster.SendGrid.Models;
using Watchster.SendGrid.Services;

namespace Watchster.Application.Features.Commands
{
    public class SendMovieRecommendationsViaEmailCommandHandler : IRequestHandler<SendMovieRecommendationsViaEmailCommand, bool>
    {
        private readonly ISendGridService sendGridService;
        private const string enpoint = "http://localhost:4200/#/movie/";

        public SendMovieRecommendationsViaEmailCommandHandler(ISendGridService sendGridService)
        {
            this.sendGridService = sendGridService;
        }

        public async Task<bool> Handle(SendMovieRecommendationsViaEmailCommand request, CancellationToken cancellationToken)
        {
            if (!request.Recommendations.Any())
            {
                return false;
            }

            var body = ConstructEmailBody(request.Recommendations);

            var mail = new MailInfo
            {
                Subject = "Watchster : Movies Recommendations",
                Body = body,
                Receiver = new EmailAddress()
                {
                    Email = request.ToEmailAddress
                }
            };

            try
            {
                await sendGridService.SendMailAsync(mail);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private static string ConstructEmailBody(IEnumerable<MovieRecommendation> recommendations)
        {
            var body = new StringBuilder();
            body.Append("Hello, <br/><br/>");
            body.Append("We believe that you might like the following movies. <br/><br/>");

            body.Append("<table style=\"width: 60%;\">");

            body.Append("<tr style=\"background-color: #ffffff;>");
            body.Append("<th style=\"border: 1px solid #dddddd; text-align: left; padding: 8px;\">&nbsp;</th>");
            body.Append("<th style=\"border: 1px solid #dddddd; text-align: left; padding: 8px;\">Title</th>");
            body.Append("<th style=\"border: 1px solid #dddddd; text-align: left; padding: 8px;\">Genres</th>");
            body.Append("<th style=\"border: 1px solid #dddddd; text-align: left; padding: 8px;\">ReleaseDate</th>");
            body.Append("<th style=\"border: 1px solid #dddddd; text-align: left; padding: 8px;\">&nbsp;</th>");
            body.Append("</tr>");

            var row = 0;
            foreach (var movie in recommendations)
            {
                if (row % 2 == 0)
                {
                    body.Append("<tr style=\"background-color: #dddddd;\">");
                }
                else
                {
                    body.Append("<tr style=\"background-color: #ffffff;\">");
                }

                body.Append($"<td style=\"border: 1px solid #dddddd; text-align: center; padding: 8px; display:block;\">{++row}</th>");
                body.Append($"<td style=\"border: 1px solid #dddddd; text-align: left; padding: 8px;\">{movie.Title}</th>");
                body.Append($"<td style=\"border: 1px solid #dddddd; text-align: left; padding: 8px;\">{movie.Genres}</th>");
                body.Append($"<td style=\"border: 1px solid #dddddd; text-align: left; padding: 8px;\">{movie.ReleaseDate}</th>");
                body.Append($"<td style=\"border: 1px solid #dddddd; text-align: center; padding: 8px;\"><a href=\"{enpoint}{movie.Id}\">View</a></th>");
                body.Append("</tr>");
            }
            body.Append("</table><br/>");

            body.Append("Watchster, <br/>");
            body.Append("Your Online Movie Recommender");

            return body.ToString();
        }
    }
}
