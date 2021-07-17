using FluentAssertions;
using NUnit.Framework;
using Pokedex.Domain.Common.Enums;
using Pokedex.Infrastructure.Integrations.FunTranslationsApi;
using Pokedex.Infrastructure.Integrations.FunTranslationsApi.Interfaces;
using RestSharp;

namespace Pokedex.Infrastructure.Integration.Tests.FunTranslationsApi
{
    [TestFixture]
    public class FunTranslationsApiClientTests
    {
        private FunTranslationsApiClient _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new FunTranslationsApiClient(new RestClient(), new IntegrationFunTranslationConfiguration());
        }

        [Test]
        [Ignore(" For public API calls this is 60 API calls a day with distribution of 5 calls an hour")]
        public void Translate_InputText_ShouldReturnTextTranslatedUsingShakespeareTranslation()
        {
            // Arrange
            var textToTranslate = "You gave Mr. Tim a hearty meal, but unfortunately what he ate made him die.";
            var textTranslated = "Thee did giveth mr. Tim a hearty meal,  but unfortunately what he did doth englut did maketh him kicketh the bucket.";

            // Act
            var serviceResponse = _sut.Translate(textToTranslate, TranslationType.Shakespeare);

            // Assert
            serviceResponse.Succeeded.Should().BeTrue();
            serviceResponse.Content.Should().BeEquivalentTo(textTranslated);
        }

    }

    public class IntegrationFunTranslationConfiguration : IFunTranslationsApiConfigurations
    {
        public string BaseUrl => "https://api.funtranslations.com";
    }
}
