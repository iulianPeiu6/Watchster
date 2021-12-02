using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Watchster.DataAccess;
using Watchster.Domain.Entities;

namespace Database.UnitTests
{
    public class AppSettingsRepositoryTests : DatabaseTestBase
    {
        private readonly Repository<AppSettings> repository;
        private readonly AppSettings newAppSettings;

        public AppSettingsRepositoryTests()
        {
            repository = new Repository<AppSettings>(context);
            newAppSettings = new AppSettings()
            {
                Id = Guid.Parse("9ca29f6b-dbb8-4e11-8a2c-4b02819750d0"),
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
        public void Given_NewAppSettings_When_NewAppSettingsIsNotNull_Then_AddAsyncShouldReturnATaskConcerningNewAppSettings()
        {
            var result = repository.AddAsync(newAppSettings);

            result.Should().BeOfType<Task<AppSettings>>();
        }

        [Test]
        public void Given_NewAppSettings_When_NewAppSettingsIsNull_Then_AddAsyncShouldThrowArgumentNullException()
        {
            AppSettings newAppSettingsNull = null;

            Action result = () => repository.AddAsync(newAppSettingsNull).Wait();

            result.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Given_AppSettings_When_AppSettingsIsInDatabase_Then_DeleteShouldReturnATaskConcerningDeletedAppSettings()
        {
            var AppSettings = new AppSettings
            {
                Section = "Test Section",
                Parameter = "Test Parameter",
                Description = "Test Description",
                Value = "Test Value"
            };

            var result = repository.Delete(AppSettings);

            result.Should().BeOfType<Task<AppSettings>>();
        }

        [Test]
        public void Given_AppSettingsDatabase_When_DatabaseIsPopulated_Then_GetAllAsyncShouldReturnATaskConcerningAllAppSettingss()
        {
            var result = repository.GetAllAsync();

            result.Should().BeOfType<Task<IEnumerable<AppSettings>>>();
        }

        [Test]
        public void Given_AppSettingsId_When_AppSettingsIdIsInDatabase_Then_GetByIdAsyncShouldReturnATaskConcerningThatAppSettings()
        {
            var id = Guid.Parse("68059ff3-d99e-43b4-9be9-699f492f1a8a");

            var result = repository.GetByIdAsync(id);

            result.Should().BeOfType<Task<AppSettings>>();
        }

        [Test]
        public void Given_AppSettingsId_When_AppSettingsIdIsNull_Then_GetByIdAsyncShouldThrowArgumentException()
        {
            Guid id = Guid.Empty;

            Action result = () => repository.GetByIdAsync(id).Wait();

            result.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Given_NewAppSettings_When_AppSettingsWasInDatabase_Then_UpdateAsyncShouldReturnATaskConcerningUpdatedAppSettings()
        {
            var result = repository.UpdateAsync(newAppSettings);

            result.Should().BeOfType<Task<AppSettings>>();
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
        }

        [Test]
        public void Given_Expression_When_AppSettingssPopulateDatabase_ThenQueryShouldReturnAQueryableCollectionOfAppSettingssRespectingThatExpression()
        {
            Expression<Func<AppSettings, bool>> expression = AppSettings => AppSettings.Description.Contains("Test");

            var result = repository.Query(expression).ToList();

            result.Should().BeOfType<List<AppSettings>>();
        }
    }
}
