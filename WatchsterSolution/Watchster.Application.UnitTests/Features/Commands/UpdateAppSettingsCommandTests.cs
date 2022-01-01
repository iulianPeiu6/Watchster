using FakeItEasy;
using Faker;
using NUnit.Framework;
using System.Threading.Tasks;
using Watchster.Application.Features.Commands;
using Watchster.Application.Interfaces;
using Watchster.Domain.Entities;

namespace Watchster.Application.UnitTests.Features.Commands
{
    public class UpdateAppSettingsCommandTests
    {
        private readonly IAppSettingsRepository appSettingsRepository;
        private readonly UpdateAppSettingsCommandHandler handler;

        public UpdateAppSettingsCommandTests()
        {
            appSettingsRepository = A.Fake<IAppSettingsRepository>();
            handler = new UpdateAppSettingsCommandHandler(appSettingsRepository);
        }

        [Test]
        public async Task Given_UpdateAppSettingsCommand_When_HandlerIsCalled_Should_CallUpdateAsync()
        {
            //arange
            var command = new UpdateAppSettingsCommand()
            {
                Id = RandomNumber.Next(),
                Description = Lorem.Sentence(),
                Parameter = Lorem.Sentence(),
                Section = Lorem.Sentence(),
                Value = Lorem.Sentence(),
            };

            //act
            var response = await handler.Handle(command, default);

            //assert
            A.CallTo(() => appSettingsRepository.UpdateAsync(A<AppSettings>._)).MustHaveHappenedOnceExactly();
        }
    }
}
