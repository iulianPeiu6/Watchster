using FakeItEasy;
using Faker;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using Watchster.Application.Features.Commands;
using Watchster.Application.UnitTests.Fakes;

namespace Watchster.Application.UnitTests.Features.Commands
{
    public class VerifyPasswordCodeCommandTests
    {
        private const int minValue = 1;
        private const int maxValue = int.MaxValue;
        private readonly FakeResetPasswordCodeRepository fakeMovieRepository;
        private readonly VerifyPasswordCodeCommandHandler handler;

        public VerifyPasswordCodeCommandTests()
        {
            fakeMovieRepository = A.Fake<FakeResetPasswordCodeRepository>();
            handler = new VerifyPasswordCodeCommandHandler(fakeMovieRepository);
        }

        [Test]
        public async Task Given_VerifyPasswordCodeCommand_When_InvalidCode_Should_FailReturnFalse()
        {
            //arrage
            var command = new VerifyPasswordCodeCommand()
            {
                Code = Lorem.Sentence(13),
            };

            //act
            var response = await handler.Handle(command, default);

            //assert
            response.Should().Be(false);
        }

        [Test]
        public async Task Given_VerifyPasswordCodeCommand_When_ValidCodeAndTimeNotExpired_Should_FailReturnTrue()
        {
            //arrage
            var command = new VerifyPasswordCodeCommand()
            {
                Code = "hardcoded",
            };

            //act
            var response = await handler.Handle(command, default);

            //assert
            response.Should().Be(true);
        }

        [Test]
        public async Task Given_VerifyPasswordCodeCommand_When_ValidCodeButExpired_Should_FailReturnFalse()
        {
            //arrage
            var command = new VerifyPasswordCodeCommand()
            {
                Code = "code without email",
            };

            //act
            var response = await handler.Handle(command, default);

            //assert
            response.Should().Be(false);
        }

    }

}
