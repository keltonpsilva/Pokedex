using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Pokedex.Application.Contracts.Response;
using Pokedex.Application.Interfaces;
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
        private Mock<ITranslationCommand> _translationCommand;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();
            _pokeApiClient = new Mock<IPokeApiClient>();
            _translationCommand = new Mock<ITranslationCommand>();

            _sut = new PokemonController(_pokeApiClient.Object, _translationCommand.Object);
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

        [Test]
        public void Translate_InvalidPokemonName_ShouldReturnsNotFoundHttpResponse()
        {
            // Arrange
            var serviceResponse = ServiceResponse<Pokemon>.Fail(null);

            _pokeApiClient.Setup(p => p.Get(It.IsAny<string>())).Returns(() => serviceResponse);

            // Act
            var actionResult = _sut.Translate(Guid.NewGuid().ToString());

            // Assert
            actionResult.Should().BeOfType<NotFoundResult>();
        }

        [Test]
        public void Translate_ValidPokemonName_ShouldReturnsOKHttpResponse()
        {
            // Arrange
            var expectedPokemon = _fixture.Create<Pokemon>();

            _pokeApiClient.Setup(p => p.Get(It.IsAny<string>())).Returns(() => ServiceResponse<Pokemon>.Success(expectedPokemon));
            _translationCommand.Setup(t => t.Execute(It.IsAny<Pokemon>())).Returns(() => expectedPokemon);

            // Act
            var actionResult = _sut.Translate(expectedPokemon.Name);
            var actionResultContent = (Pokemon)(actionResult as OkObjectResult).Value;

            // Assert
            actionResult.Should().BeOfType<OkObjectResult>();
            actionResultContent.Should().NotBeNull();
            actionResultContent.Should().BeEquivalentTo(expectedPokemon);
        }
    }
}
