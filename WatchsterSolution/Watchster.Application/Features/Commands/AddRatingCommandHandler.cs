using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Watchster.Application.Interfaces;
using Watchster.Application.Models;
using Watchster.Domain.Entities;

namespace Watchster.Application.Features.Commands
{
    public class AddRatingCommandHandler : IRequestHandler<AddRatingCommand, AddRatingResponse>
    {
        private readonly double upperLimit = 10;
        private readonly double downLimit = 0;
        private readonly IMovieRepository movieRepository;
        private readonly IRatingRepository ratingRepository;
        private readonly IUserRepository userRepository;
        public AddRatingCommandHandler(IMovieRepository movieRepository, IRatingRepository ratingRepository, IUserRepository userRepository)
        {
            this.movieRepository = movieRepository;
            this.ratingRepository = ratingRepository;
            this.userRepository = userRepository;
        }
        public async Task<AddRatingResponse> Handle(AddRatingCommand request, CancellationToken cancellationToken)
        {
            var rating = new Rating
            {
                RatingValue = request.rating,
                MovieId = request.movieId,
                UserId = request.userId
            };
            var response = new AddRatingResponse();

            var userInstance = await Task.Run(async () =>
            {
                return userRepository.Query(user => user.Id == request.userId).FirstOrDefault();
            });
            if (userInstance is null)
            {
                response.ErrorMessage = Error.UserNotFound;
                response.IsSuccess = false;
                return response;
            }

            var movieInstance = await Task.Run(async () =>
            {
                return movieRepository.Query(movie => movie.Id == request.movieId).FirstOrDefault();
            });

            if(movieInstance is null)
            {
                response.ErrorMessage = Error.MovieNotFound;
                response.IsSuccess = false;
                return response;
            }

            var ratingInstance = await Task.Run(async () =>
            {
                return ratingRepository.Query(rating => (rating.MovieId == request.movieId) && (rating.UserId == request.userId)).FirstOrDefault();
            });

            if(ratingInstance is not null)
            {
                response.ErrorMessage = Error.MovieAlreadyRated;
                response.IsSuccess = false;
                return response;
            }

            if (!(request.rating >= downLimit && request.rating <= upperLimit))
            {
                response.ErrorMessage = Error.RatingNotInRange;
                response.IsSuccess = false;
                return response;
            }
            
            await ratingRepository.AddAsync(rating);
            response.IsSuccess = true;
            return response;   
        }
    }
}
