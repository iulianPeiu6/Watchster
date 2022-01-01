using FakeItEasy;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Watchster.Application.Features.Queries;
using Watchster.Application.Interfaces;

namespace Watchster.Application.UnitTests.Features.Queries
{
    public class GetAllUsersQueryTests
    {
        private readonly GetAllUsersQueryHandler handler;
        private readonly IUserRepository ratingRepository;

        public GetAllUsersQueryTests()
        {
            ratingRepository = A.Fake<IUserRepository>();
            handler = new GetAllUsersQueryHandler(ratingRepository);
        }

        [Test]
        public async Task Given_GetAllRatingsQuery_When_GetAllRatingsQueryHandlerIsCalled_Should_GetAllAsyncIsCalledAsync()
        {
            var query = new GetAllUsersQuery();

            var response = await handler.Handle(query, default);

            A.CallTo(() => ratingRepository.GetAllAsync()).MustHaveHappenedOnceExactly();
        }
    }
}
