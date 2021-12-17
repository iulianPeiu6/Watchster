using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Watchster.DataAccess;
using Watchster.DataAccess.Repositories;
using Watchster.Domain.Entities;

namespace Database.UnitTests
{
    class UserRepositoryTests : DatabaseTestBase
    {
        private readonly UserRepository repository;
        private readonly User newUser;

        public UserRepositoryTests()
        {
            repository = new UserRepository(context);

            var rating = new Rating
            {
                UserId = 1,
                MovieId = 1,
                RatingValue = 9.10
            };
            List<Rating> UserTestRatings = new List<Rating>();

            newUser = new User()
            {
                Id = 1,
                Email = "Unit Test Email",
                Password = "Unit Test Password",
                IsSubscribed = true,
                RegistrationDate = new DateTime(2021, 12, 4),
                UserRatings = UserTestRatings
            };
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Given_NewUser_When_NewUserIsNotNull_Then_AddAsyncShouldReturnATaskConcerningNewUser()
        {
            var result = repository.AddAsync(newUser);

            result.Should().BeOfType<Task<User>>();
        }

        [Test]
        public void Given_NewUser_When_NewUserIsNull_Then_AddAsyncShouldThrowArgumentNullException()
        {
            User newUserNull = null;

            Action result = () => repository.AddAsync(newUserNull).Wait();

            result.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Given_User_When_UserIsInDatabase_Then_DeleteShouldReturnATaskConcerningDeletedUser()
        {
            var rating = new Rating
            {
                UserId = 1,
                MovieId = 2,
                RatingValue = 8.9
            };
            List<Rating> UserRatings = new List<Rating>();
            UserRatings.Add(rating);
            var User = new User
            {
                Id = 3,
                Email = "UserTestEmail@yahoo.com",
                Password = "TestPassword",
                IsSubscribed = true,
                RegistrationDate = new DateTime(2021, 12, 1),
                UserRatings = UserRatings
            };

            var result = repository.Delete(User);

            result.Should().BeOfType<Task<User>>();
        }

        [Test]
        public void Given_UserDatabase_When_DatabaseIsPopulated_Then_GetAllAsyncShouldReturnATaskConcerningAllUsers()
        {
            var result = repository.GetAllAsync();

            result.Should().BeOfType<Task<IEnumerable<User>>>();
        }

        [Test]
        public void Given_UserId_When_UserIdIsInDatabase_Then_GetByIdAsyncShouldReturnATaskConcerningThatUser()
        {
            var id = 1;

            var result = repository.GetByIdAsync(id);

            result.Should().BeOfType<Task<User>>();
        }

        [Test]
        public void Given_UserId_When_UserIdIsNull_Then_GetByIdAsyncShouldThrowArgumentException()
        {
            var id = -1;

            Action result = () => repository.GetByIdAsync(id).Wait();

            result.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Given_NewUser_When_UserWasInDatabase_Then_UpdateAsyncShouldReturnATaskConcerningUpdatedUser()
        {
            var result = repository.UpdateAsync(newUser);

            result.Should().BeOfType<Task<User>>();
        }

        [Test]
        public void Given_NewUser_When_UserIsNull_Then_UpdateAsyncShouldThrowArgumentNullException()
        {
            User newUserNull = null;

            Action result = () => repository.UpdateAsync(newUserNull).Wait();

            result.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Given_Users_When_UsersPopulateDatabase_Then_QueryShouldReturnAQueryableCollectionOfUsers()
        {
            var result = repository.Query().ToList();

            result.Should().BeOfType<List<User>>();
        }

        [Test]
        public void Given_Expression_When_UsersPopulateDatabase_ThenQueryShouldReturnAQueryableCollectionOfUsersRespectingThatExpression()
        {
            Expression<Func<User, bool>> expression = User => User.Email.Contains("Test");

            var result = repository.Query(expression).ToList();

            result.Should().BeOfType<List<User>>();
        }
    }
}
