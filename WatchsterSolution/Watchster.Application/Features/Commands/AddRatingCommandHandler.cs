using MediatR;
using System.Linq;
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
                RatingValue = request.Rating,
                MovieId = request.MovieId,
                UserId = request.UserId
            };
            var response = new AddRatingResponse();

            var userInstance = await userRepository.GetByIdAsync(request.UserId);

            if (userInstance is null)
            {
                response.ErrorMessage = Error.UserNotFound;
                response.IsSuccess = false;
                return response;
            }

            var movieInstance = await movieRepository.GetByIdAsync(request.MovieId);

            if (movieInstance is null)
            {
                response.ErrorMessage = Error.MovieNotFound;
                response.IsSuccess = false;
                return response;
            }

            var ratingInstance = ratingRepository
                .Query(rating => (rating.MovieId == request.MovieId) && (rating.UserId == request.UserId))
                .FirstOrDefault();

            if (ratingInstance is not null)
            {
                response.ErrorMessage = Error.MovieAlreadyRated;
                response.IsSuccess = false;
                return response;
            }

            if (!(request.Rating >= downLimit && request.Rating <= upperLimit))
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
