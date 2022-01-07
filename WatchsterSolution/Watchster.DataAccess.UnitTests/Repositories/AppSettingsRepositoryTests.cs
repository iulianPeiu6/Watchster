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
    public class AppSettingsRepositoryTests : DatabaseTestBase
    {
        private AppSettingsRepository repository;
        private readonly AppSettings newAppSettings;

        public AppSettingsRepositoryTests()
        {
            repository = new AppSettingsRepository(context);
            newAppSettings = new AppSettings()
            {
                Id = 3,
                Description = "Unit Test Description",
                Section = "Unit Test Section",
                Parameter = "Unit Test Parameter",
                Value = "Unit Test Value"
            };
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Given_NewAppSettings_When_NewAppSettingsIsNotNull_Then_AddAsyncShouldAddNewAppSettings()
        {
            var result = await repository.AddAsync(newAppSettings);
            var addAppSettings = await repository.GetByIdAsync(3);

            result.Should().BeOfType<AppSettings>();
            addAppSettings.Description.Should().Be(newAppSettings.Description);
            addAppSettings.Section.Should().Be(newAppSettings.Section);
            addAppSettings.Parameter.Should().Be(newAppSettings.Parameter);
            addAppSettings.Value.Should().Be(newAppSettings.Value);
        }

        [Test]
        public void Given_NewAppSettings_When_NewAppSettingsIsNull_Then_AddAsyncShouldThrowArgumentNullException()
        {
            AppSettings newAppSettingsNull = null;

            Action result = () => repository.AddAsync(newAppSettingsNull).Wait();

            result.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public async Task Given_AppSettings_When_AppSettingsIsInDatabase_Then_DeleteShouldRemoveAppSettings()
        {
            var AppSettings = new AppSettings
            {
                Id = 1,
                Section = "Test Section",
                Parameter = "Test Parameter",
                Description = "Test Description",
                Value = "Test Value"
            };

            var result = await repository.Delete(AppSettings);
            var deletedAppSettings = await repository.GetByIdAsync(1);

            result.Should().BeOfType<AppSettings>();
            deletedAppSettings.Should().BeNull();
        }

        [Test]
        public async Task Given_AppSettingsDatabase_When_DatabaseIsPopulated_Then_GetAllAsyncShouldReturnAListOfAppSettings()
        {
            var result = await repository.GetAllAsync();

            result.Should().BeOfType<List<AppSettings>>();
            result.Count().Should().Be(1);
        }

        [Test]
        public async Task Given_AppSettingsId_When_AppSettingsIdIsInDatabase_Then_GetByIdAsyncShouldReturnThatAppSettings()
        {
            var id = 2;

            var result = await repository.GetByIdAsync(id);

            result.Should().BeOfType<AppSettings>();
            result.Section.Should().Be("Test Section 2");
            result.Parameter.Should().Be("Test Parameter 2");
            result.Description.Should().Be("Test Description 2");
            result.Value.Should().Be("Test Value 2");
        }

        [Test]
        public void Given_AppSettingsId_When_AppSettingsIdIsNull_Then_GetByIdAsyncShouldThrowArgumentException()
        {
            var id = -1;

            Action result = () => repository.GetByIdAsync(id).Wait();

            result.Should().Throw<ArgumentException>();
        }

        [Test]
        public async Task Given_NewAppSettings_When_AppSettingsWasInDatabase_Then_UpdateAsyncShouldUpdateAppSettings()
        {
            var appSettings = new AppSettings()
            {
                Id = 2,
                Section = "New Test Section",
                Parameter = "Test Parameter 2",
                Description = "Test Description 2",
                Value = "Test Value 2"
            };
            var result = await repository.UpdateAsync(appSettings);
            var updatedAppSettings = await repository.GetByIdAsync(appSettings.Id);

            result.Should().BeOfType<AppSettings>();
            updatedAppSettings.Section.Should().Be(appSettings.Section);
        }

        [Test]
        public void Given_NewAppSettings_When_AppSettingsIsNull_Then_UpdateAsyncShouldThrowArgumentNullException()
        {
            AppSettings newAppSettingsNull = null;

            Action result = () => repository.UpdateAsync(newAppSettingsNull).Wait();

            result.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Given_AppSettingss_When_AppSettingssPopulateDatabase_Then_QueryShouldReturnAQueryableCollectionOfAppSettingss()
        {
            var result = repository.Query().ToList();

            result.Should().BeOfType<List<AppSettings>>();
            result.Count.Should().Be(1);
        }

        [Test]
        public void Given_Expression_When_AppSettingssPopulateDatabase_ThenQueryShouldReturnAQueryableCollectionOfAppSettingssRespectingThatExpression()
        {
            Expression<Func<AppSettings, bool>> expression = AppSettings => AppSettings.Description.Contains("Test");

            var result = repository.Query(expression).ToList();

            result.Should().BeOfType<List<AppSettings>>();
            result.Count.Should().Be(1);
        }
    }
}
