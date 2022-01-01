using Faker;
using FluentAssertions;
using NUnit.Framework;
using System;
using Watchster.Application.Interfaces;
using Watchster.Application.Utils.Cryptography;

namespace Watchster.Application.UnitTests.Utils.Cryptography
{
    public class CryptographyServiceTests
    {
        private readonly ICryptographyService cryptographyService;

        public CryptographyServiceTests()
        {
            cryptographyService = new CryptographyService();
        }

        [Test]
        public void Given_Password_When_IsHashedUsingSHA3_Should_ReturnAHash()
        {
            var password = Lorem.Sentence(new Random().Next(1, 10));

            var hashedPassword = cryptographyService.GetPasswordSHA3Hash(password);

            password.Should().NotBeNullOrEmpty();
            hashedPassword.Length.Should().Be(44);
        }
    }
}
