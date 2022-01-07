using FakeItEasy;
using Faker;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Watchster.Application.Features.Queries;
using Watchster.Application.Interfaces;
using Watchster.Domain.Entities;

namespace Watchster.Application.UnitTests.Features.Queries
{
    public class GetUserDetailsQueryTests
    {
        private readonly GetUserDetailsQueryHandler handler;
        private readonly IUserRepository userRepository;
        private readonly IRatingRepository ratingRepository;
        private const int validUserId = 1;
        private const int invalidUserId = -1;
        private readonly User validUser;

        public GetUserDetailsQueryTests()
        {
            validUser = new User
            {
                Id = validUserId,
                Email = Internet.Email(),
                IsSubscribed = true,
                Password = Lorem.Sentence(),
                RegistrationDate = DateTime.Now
            };
            var fakeUserRepository = new Fake<IUserRepository>();
            fakeUserRepository.CallsTo(ur => ur.GetByIdAsync(invalidUserId))
                .Returns(Task.FromResult<User>(null));
            fakeUserRepository.CallsTo(ur => ur.GetByIdAsync(validUserId))
                .Returns(Task.FromResult(validUser));
            userRepository = fakeUserRepository.FakedObject;
            ratingRepository = A.Fake<IRatingRepository>();
            handler = new GetUserDetailsQueryHandler(userRepository, ratingRepository);
        }

        [Test]
        public void Given_GetUserDetailsQueryWithInvalidId_When_GetAllMoviesQueryHandlerIsCalled_Then_HandlerShouldThrowArgumentExceptionAsync()
        {
            // Arrange
            var query = new GetUserDetailsQuery
            {
                UserId = invalidUserId
            };

            // Act
            Action call = () => handler.Handle(query, default).Wait();

            // Assert
            call.Should().Throw<ArgumentException>().WithMessage("User not found!");
        }

        [Test]
        public async Task Given_GetUserDetailsQueryWithValidId_When_GetAllMoviesQueryHandlerIsCalled_Then_HandlerShouldReturnApopiateResultAsync()
        {
            // Arrange
            var query = new GetUserDetailsQuery
            {
                UserId = validUserId
            };

            // Act
            var response = await handler.Handle(query, default);

            // Assert
            response.Should().NotBeNull();
            response.Id.Should().Be(validUser.Id);
            response.IsSubscribed.Should().Be(validUser.IsSubscribed);
            response.NumberOfTotalGivenRatings.Should().Be(0);
            response.RegistrationDate.Should().Be(validUser.RegistrationDate);
            response.Email.Should().Be(validUser.Email);
        }
    }
}
