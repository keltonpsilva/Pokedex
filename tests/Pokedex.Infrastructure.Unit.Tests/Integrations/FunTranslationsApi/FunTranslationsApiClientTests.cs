using AutoFixture;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Pokedex.Domain.Common.Enums;
using Pokedex.Infrastructure.Integrations.FunTranslationsApi;
using Pokedex.Infrastructure.Integrations.FunTranslationsApi.Contracts.Response.v1;
using Pokedex.Infrastructure.Integrations.FunTranslationsApi.Interfaces;
using RestSharp;
using System;
using System.Net;

namespace Pokedex.Infrastructure.Unit.Tests.Integrations.FunTranslationsApi
{
    [TestFixture]
    public class FunTranslationsApiClientTests
    {
        private FunTranslationsApiClient _sut;
        private Fixture _fixture;

        private Mock<IRestClient> restClient;
        private Mock<IFunTranslationsApiConfigurations> funTranslationsApiConfigurations;

        [SetUp]
        public void SetUp()
        {
            var baseUrl = $"http://{Guid.NewGuid()}";

            _fixture = new Fixture();

            restClient = new Mock<IRestClient>();
            restClient.Setup(r => r.BaseUrl).Returns(new Uri(baseUrl));


            funTranslationsApiConfigurations = new Mock<IFunTranslationsApiConfigurations>();
            funTranslationsApiConfigurations.Setup(p => p.BaseUrl).Returns(baseUrl);



            _sut = new FunTranslationsApiClient(restClient.Object, funTranslationsApiConfigurations.Object);

        }

        [Test]
        public void Translate_InputText_ShouldReturnFailResponse()
        {
            // Arrange
            var textToTranslate = Guid.NewGuid().ToString();

            var apiReponse = _fixture.Build<RestResponse<FunTranslationsResponse>>()
                                     .With(p => p.ResponseStatus, ResponseStatus.Completed)
                                     .OmitAutoProperties()
                                     .Create();

            restClient.Setup(r => r.Execute<FunTranslationsResponse>(It.IsAny<RestRequest>()))
                      .Returns(() => apiReponse);

            // Act
            var serviceResponse = _sut.Translate(textToTranslate, TranslationType.Shakespeare);

            // Assert
            serviceResponse.Failed.Should().BeTrue();
            serviceResponse.Content.Should().BeNull();
        }

        [Test]
        public void Translate_InputText_ShouldReturnTextTranslatedUsingShakespeareTranslation()
        {
            // Arrange
            var textToTranslate = "You gave Mr. Tim a hearty meal, but unfortunately what he ate made him die.";
            var textTranslated = "Thee did giveth mr. Tim a hearty meal,  but unfortunately what he did doth englut did maketh him kicketh the bucket.";

            var expectedTranslationResponse = _fixture.Build<FunTranslationsResponse>()
                             .With(p => p.Contents, new Contents { Text = textToTranslate, Translated = textTranslated })
                             .Create();

            var apiReponse = _fixture.Build<RestResponse<FunTranslationsResponse>>()
                                     .With(p => p.StatusCode, HttpStatusCode.OK)
                                     .With(p => p.ResponseStatus, ResponseStatus.Completed)
                                     .With(p => p.Data, expectedTranslationResponse)
                                     .With(p => p.Request, new RestRequest { Resource = Guid.NewGuid().ToString() })
                                     .Without(p => p.ErrorException)
                                     .Without(p => p.ErrorMessage)
                                     .WithAutoProperties()
                                     .Create();

            restClient.Setup(r => r.Execute<FunTranslationsResponse>(It.IsAny<RestRequest>()))
                      .Returns(() => apiReponse);

            // Act
            var serviceResponse = _sut.Translate(textToTranslate, TranslationType.Shakespeare);

            // Assert
            serviceResponse.Succeeded.Should().BeTrue();
            serviceResponse.Content.Should().NotBeNull();
            serviceResponse.Content.Should().BeEquivalentTo(expectedTranslationResponse.Contents.Translated);
        }

    }
}
