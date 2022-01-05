using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Watchster.Application.Interfaces;
using Watchster.Application.Models;

namespace Watchster.Application.Features.Queries
{
    public class GetUserDetailsQueryHandler : IRequestHandler<GetUserDetailsQuery, GetUserDetailsResponse>
    {
        private readonly IUserRepository userRepository;
        private readonly IRatingRepository ratingRepository;

        public GetUserDetailsQueryHandler(IUserRepository userRepository, IRatingRepository ratingRepository)
        {
            this.userRepository = userRepository;
            this.ratingRepository = ratingRepository;
        }

        public async Task<GetUserDetailsResponse> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
        {
            var user = await userRepository
                .GetByIdAsync(request.UserId);

            if (user == null)
            {
                throw new ArgumentException("User not found!");
            }

            var numberOfTotalRatingsGiven = ratingRepository.Query(rating => rating.UserId == request.UserId)
                .ToList()
                .Count;

            return new GetUserDetailsResponse
            {
                Id = request.UserId,
                Email = user.Email,
                IsSubscribed = user.IsSubscribed,
                RegistrationDate = user.RegistrationDate,
                NumberOfTotalGivenRatings = numberOfTotalRatingsGiven,
            };
        }
    }
}
