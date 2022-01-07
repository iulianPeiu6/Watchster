using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
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
                UserId = 3,
                MovieId = 1,
                RatingValue = 9.10
            };
            List<Rating> UserTestRatings = new List<Rating>();

            newUser = new User()
            {
                Id = 3,
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
        public async Task Given_NewUser_When_NewUserIsNotNull_Then_AddAsyncShouldAddNewUser()
        {
            var result = await repository.AddAsync(newUser);
            var addedUser = await repository.GetByIdAsync(newUser.Id);

            result.Should().BeOfType<User>();
            addedUser.Email.Should().Be(newUser.Email);
            addedUser.Password.Should().Be(newUser.Password);
            addedUser.RegistrationDate.Should().Be(newUser.RegistrationDate);
        }

        [Test]
        public void Given_NewUser_When_NewUserIsNull_Then_AddAsyncShouldThrowArgumentNullException()
        {
            User newUserNull = null;

            Action result = () => repository.AddAsync(newUserNull).Wait();

            result.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public async Task Given_User_When_UserIsInDatabase_Then_DeleteShouldDeleteUser()
        {
            var rating = new Rating
            {
                Id = 1,
                UserId = 1,
                MovieId = 1,
                RatingValue = 8.9
            };
            List<Rating> userRatings = new List<Rating>();
            userRatings.Add(rating);
            var user = new User
            {
                Id = 1,
                Email = "UserTestEmail@yahoo.com",
                Password = "TestPassword",
                IsSubscribed = true,
                RegistrationDate = new DateTime(2021, 12, 1),
                UserRatings = userRatings
            };

            var result = await repository.Delete(user);
            var deletedUser = await repository.GetByIdAsync(user.Id);

            result.Should().BeOfType<User>();
            deletedUser.Should().BeNull();
        }

        [Test]
        public async Task Given_UserDatabase_When_DatabaseIsPopulated_Then_GetAllAsyncShouldReturnAllUsers()
        {
            var result = await repository.GetAllAsync();

            result.Should().BeOfType<List<User>>();
            result.Count().Should().Be(2);
        }

        [Test]
        public async Task Given_UserId_When_UserIdIsInDatabase_Then_GetByIdAsyncShouldThatUser()
        {
            var result = await repository.GetByIdAsync(newUser.Id);

            result.Should().BeOfType<User>();
            result.Email.Should().Be(newUser.Email);
            result.Password.Should().Be(newUser.Password);
            result.RegistrationDate.Should().Be(newUser.RegistrationDate);
        }

        [Test]
        public void Given_UserId_When_UserIdIsNull_Then_GetByIdAsyncShouldThrowArgumentException()
        {
            var id = -1;

            Action result = () => repository.GetByIdAsync(id).Wait();

            result.Should().Throw<ArgumentException>();
        }

        [Test]
        public async Task Given_NewUser_When_UserWasInDatabase_Then_UpdateAsyncShouldUpdateUser()
        {
            var rating = new Rating
            {
                Id = 2,
                UserId = 2,
                MovieId = 1,
                RatingValue = 9.0
            };
            List<Rating> user2Ratings = new List<Rating>();
            user2Ratings.Add(rating);
            var user = new User
            {
                Id = 2,
                Email = "UserTestEmail2@yahoo.com",
                Password = "Updated Password",
                IsSubscribed = true,
                RegistrationDate = new DateTime(2021, 12, 1),
                UserRatings = user2Ratings
            };

            var result = await repository.UpdateAsync(user);
            var updatedUser = await repository.GetByIdAsync(user.Id);

            result.Should().BeOfType<User>();
            updatedUser.Password.Should().Be(user.Password);
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
            result.Count.Should().Be(2);
        }

        [Test]
        public void Given_Expression_When_UsersPopulateDatabase_ThenQueryShouldReturnAQueryableCollectionOfUsersRespectingThatExpression()
        {
            Expression<Func<User, bool>> expression = User => User.Email.Contains("Test");

            var result = repository.Query(expression).ToList();

            result.Should().BeOfType<List<User>>();
            result.Count.Should().Be(2);
        }
    }
}
