using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Pokedex.Application.Contracts.Response;
using Pokedex.Domain.DomainModel;
using Pokedex.Infrastructure.Integrations.PokeApi.Interfaces;
using Pokedex.WebApi.Controllers;
using System;

namespace Pokedex.WebApi.Unit.Tests.Controllers
{
    [TestFixture]
    public class PokemonControllerTests
    {
        private PokemonController _sut;
        private Fixture _fixture;
        private Mock<IPokeApiClient> _pokeApiClient;

        [SetUp]
        public void SetUp()
        {
            _pokeApiClient = new Mock<IPokeApiClient>();
            _fixture = new Fixture();

            _sut = new PokemonController(_pokeApiClient.Object);
        }

        [Test]
        public void Get_ValidPokemonName_ShouldReturnsOKHttpResponse()
        {
            // Arrange
            var expectedPokemon = _fixture.Create<Pokemon>();
            var serviceResponse = ServiceResponse<Pokemon>.Success(expectedPokemon);

            _pokeApiClient.Setup(p => p.Get(It.IsAny<string>())).Returns(() => serviceResponse);

            // Act
            var actionResult = _sut.Get(expectedPokemon.Name);
            var actionResultContent = (Pokemon)(actionResult as OkObjectResult).Value;

            // Assert
            actionResult.Should().BeOfType<OkObjectResult>();
            actionResultContent.Should().NotBeNull();
            actionResultContent.Should().BeEquivalentTo(expectedPokemon);
        }


        [Test]
        public void Get_InvalidPokemonName_ShouldReturnsNotFoundHttpResponse()
        {
            // Arrange
            var serviceResponse = ServiceResponse<Pokemon>.Fail(null);

            _pokeApiClient.Setup(p => p.Get(It.IsAny<string>())).Returns(() => serviceResponse);

            // Act
            var actionResult = _sut.Get(Guid.NewGuid().ToString());

            // Assert
            actionResult.Should().BeOfType<NotFoundResult>();
        }


    }
}
