﻿using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Watchster.Application.Utils.ML;

namespace Watchster.Application.UnitTests.Utils.ML
{
    public class MLUtilServiceCollectionExtensionsTests
    {
        private IServiceCollection services;

        public MLUtilServiceCollectionExtensionsTests()
        {
            services = A.Fake<IServiceCollection>();
        }

        [Test]
        public void Given_Services_When_AddMLUtil_Should_PutMlUtilServicesInServiceCollection()
        {
            var resultServices = services.AddMLUtil();

            resultServices.Should().NotBeNull();
            resultServices.Should().BeSameAs(services);
            //A.CallTo(() => services.AddTransient<IMovieRecommender, IMovieRecommender>()).MustHaveHappenedOnceExactly();
        }
    }
}
