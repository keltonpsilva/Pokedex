using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Pokedex.Infraclassure.Integrations.PokeApi.Contracts.Response.v2;
using Pokedex.Infrastructure.Integrations.Mappers;
using Pokedex.Infrastructure.Integrations.PokeApi;
using Pokedex.Infrastructure.Integrations.PokeApi.Interfaces;
using RestSharp;
using System;
using System.Net;

namespace Pokedex.Infrastructure.Unit.Tests.Integrations.PokeApi
{
    [TestFixture]
    public class PokeApiClientTests
    {

        private PokeApiClient _sut;
        private Fixture _fixture;

        private Mock<IRestClient> restClient;
        private Mock<IPokeApiConfigurations> pokeApiConfigurations;

        [SetUp]
        public void SetUp()
        {
            var baseUrl = $"http://{Guid.NewGuid()}";

            _fixture = new Fixture();

            restClient = new Mock<IRestClient>();

            restClient.Setup(r => r.BaseUrl).Returns(new Uri(baseUrl));

            pokeApiConfigurations = new Mock<IPokeApiConfigurations>();
            pokeApiConfigurations.Setup(p => p.BaseUrl).Returns(baseUrl);

            _sut = new PokeApiClient(restClient.Object, pokeApiConfigurations.Object);

        }

        [Test]
        public void Get_ValidPokemonName_ShouldReturnAPokemon()
        {
            // Arrange
            var pokemonName = "ditto";
            var expectedPokemonResponse = _fixture.Build<PokemonSpeciesResponse>()
                                         .With(p => p.Name, pokemonName)
                                         .Create();

            _fixture.Customize(new AutoMoqCustomization());

            var apiReponse = _fixture.Build<RestResponse<PokemonSpeciesResponse>>()
                                     .With(p => p.Data, expectedPokemonResponse)
                                     .With(p => p.StatusCode, HttpStatusCode.OK)
                                     .With(p => p.ResponseStatus, ResponseStatus.Completed)
                                     .With(p => p.Request, new RestRequest { Resource = Guid.NewGuid().ToString() })
                                     .Without(p => p.ErrorException)
                                     .Without(p => p.ErrorMessage)
                                     .WithAutoProperties()
                                     .Create();

            var expectedPokemon = expectedPokemonResponse.ToPokemon();

            restClient.Setup(r => r.Execute<PokemonSpeciesResponse>(It.IsAny<RestRequest>()))
                      .Returns(() => apiReponse);

            // Act
            var serviceResponse = _sut.Get(pokemonName);

            // Assert
            serviceResponse.Content.Should().BeOfType(typeof(Domain.DomainModel.Pokemon));
            serviceResponse.Content.Should().NotBeNull();
            serviceResponse.Content.Should().BeEquivalentTo(expectedPokemon);
        }

        [Test]
        public void Get_InvalidPokemonName_ShouldReturnFailResponse()
        {
            // Arrange
            var pokemonName = Guid.NewGuid().ToString();

            var apiReponse = _fixture.Build<RestResponse<PokemonSpeciesResponse>>()
                                     .With(p => p.StatusCode, HttpStatusCode.NotFound)
                                     .With(p => p.ResponseStatus, ResponseStatus.Completed)
                                     .OmitAutoProperties()
                                     .Create();

            restClient.Setup(r => r.Execute<PokemonSpeciesResponse>(It.IsAny<RestRequest>()))
                      .Returns(() => apiReponse);

            // Act
            var serviceResponse = _sut.Get(pokemonName);

            // Assert
            serviceResponse.Failed.Should().BeTrue();
            serviceResponse.Content.Should().BeNull();
        }

    }
}
