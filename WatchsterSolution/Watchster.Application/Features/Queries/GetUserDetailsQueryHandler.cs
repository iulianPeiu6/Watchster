using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Watchster.Application.Interfaces;
using Watchster.Application.Models;

namespace Watchster.Application.Features.Queries
{
    class GetUserDetailsQueryHandler : IRequestHandler<GetUserDetailsQuery, GetUserDetailsResponse>
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
            if (await userRepository.GetByIdAsync(request.UserId) == null)
            {
                throw new ArgumentException("The specified user does not have any ratings in the database");
            }
            var userDetails = await userRepository
                .GetByIdAsync(request.UserId);
            var numberOfTotalRatingsGiven = userRepository.Query(x => x.Id == request.UserId)
                .Join(ratingRepository.Query(), x => x.Id, y => y.Id, (x, y) => (x.Id == y.Id))
                .ToList().Count;


            return new GetUserDetailsResponse
            {
                Id = request.UserId,
                Email = userDetails.Email,
                IsSubscribed = userDetails.IsSubscribed,
                RegistrationDate = userDetails.RegistrationDate,
                NumberOfTotalRatingsGiven = numberOfTotalRatingsGiven,
            };
        }
    }
}
