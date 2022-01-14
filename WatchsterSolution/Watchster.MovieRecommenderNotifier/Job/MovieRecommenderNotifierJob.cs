using MediatR;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Watchster.Application.Features.Commands;
using Watchster.Application.Features.Queries;
using Watchster.Application.Models;
using Watchster.Domain.Entities;
using Watchster.TMDb.Services;

namespace Watchster.MovieRecommenderNotifier.Job
{
    public class MovieRecommenderNotifierJob : IJob
    {
        private readonly ILogger<MovieRecommenderNotifierJob> logger;
        private readonly IMediator mediator;
        private const int topRecommendations = 5;

        public MovieRecommenderNotifierJob(ILogger<MovieRecommenderNotifierJob> logger,
            IMediator mediator)
        {
            this.logger = logger;
            this.mediator = mediator;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            logger.LogInformation("MovieRecommenderJob Started!");

            var userToNotify = await GetSubscribedUsers();
            foreach (var user in userToNotify)
            {
                var recommendations = await GetRecommendationForUser(user);
                await SendRecommendationsToUserAsync(recommendations, user);
            }

            logger.LogInformation("MovieRecommenderJob Ended!");
        }

        private async Task<IEnumerable<User>> GetSubscribedUsers()
        {
            logger.LogInformation("Listing subscribed users");

            var query = new GetAllUsersQuery { };

            var users = await mediator.Send(query);
            var subscribedUsers = users.Where(user => user.IsSubscribed);

            return subscribedUsers;
        }

        private async Task<IEnumerable<MovieRecommendation>> GetRecommendationForUser(User user)
        {
            logger.LogInformation($"Get Recommendations for user with id {user.Id}");

            var query = new GetRecommendationsQuery
            {
                UserId = user.Id
            };

            var recommendationsDetails = await mediator.Send(query);
            var movies = recommendationsDetails.Recommendations
                .Take(topRecommendations);

            return movies;
        }

        private async Task SendRecommendationsToUserAsync(IEnumerable<MovieRecommendation> recommendations, User user)
        {
            logger.LogInformation($"Sending movie recommendation for user {user.Id} to email address {user.Email}");
            
            var command = new SendMovieRecommendationsViaEmailCommand
            {
                Recommendations = recommendations,
                ToEmailAddress = user.Email
            };

            var emailSent = await mediator.Send(command);

            logger.LogInformation($"Email was {(emailSent?string.Empty: "not ")}for user {user.Id}");
        }
    }
}
