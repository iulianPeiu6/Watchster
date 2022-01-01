using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Watchster.Application.Features.Queries;
using Watchster.Application.UnitTests.Fakes;
using Watchster.Domain.Entities;

namespace Watchster.Application.UnitTests.Features.Queries
{
    public class GetAppSettingsBySectionAndParameterQueryTests
    {
        private readonly GetAppSettingsBySectionAndParameterQueryHandler handler;
        private readonly FakeAppSettingsRepository appSettingsRepository;

        public GetAppSettingsBySectionAndParameterQueryTests()
        {
            appSettingsRepository = A.Fake<FakeAppSettingsRepository>();
            handler = new GetAppSettingsBySectionAndParameterQueryHandler(appSettingsRepository);
        }

        [Test]
        public async Task Given_GetAppSettingsBySectionAndParameterQuery_When_GetAppSettingsBySectionAndParameterQueryHandlerIsCalled_Should_GetAllAsyncIsCalledAsync()
        {
            var appSettings = new AppSettings()
            {
                Parameter = "LastSyncDate",
                Section = "MovieImporter",
                Value = "value"
            };
            await appSettingsRepository.AddAsync(appSettings);

            var query = new GetAppSettingsBySectionAndParameterQuery()
            {
                Parameter = "LastSyncDate",
                Section = "MovieImporter"
            };
            var response = await handler.Handle(query, default);

            response.Should().BeOfType<AppSettings>()
                .And.NotBeNull();
        }

        [Test]
        public async Task Given_GetAppSettingsBySectionAndParameterQuery_When_AppSettingsDoesntExist_Should_ThrowArgumentExceptionAsync()
        {
            var query = new GetAppSettingsBySectionAndParameterQuery()
            {
                Parameter = "LastSyncDate",
                Section = "MovieImporter"
            };

            await handler.Invoking(async handler => await handler.Handle(query, default))
                .Should().ThrowAsync<ArgumentException>();
        }
    }
}
