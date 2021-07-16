using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Pokedex.Application.Contracts.Response;
using Pokedex.Domain.Common.Enums;
using Pokedex.Domain.DomainModel;
using Pokedex.Infrastructure.Commands.Translations;
using Pokedex.Infrastructure.Integrations.FunTranslationsApi.Interfaces;
using System;

namespace Pokedex.Infrastructure.Unit.Tests.Commands.Translations
{
    [TestFixture]
    public class FunTranslationsTranslationCommandTests
    {
        private FunTranslationsTranslationCommand _sut;

        private Fixture _fixture;

        private Mock<IFunTranslationsApiClient> funTranslationsApiClient;
        private Mock<ILogger<FunTranslationsTranslationCommand>> logger;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();

            funTranslationsApiClient = new Mock<IFunTranslationsApiClient>();
            logger = new Mock<ILogger<FunTranslationsTranslationCommand>>();

            _sut = new FunTranslationsTranslationCommand(funTranslationsApiClient.Object, logger.Object);
        }

        [Test]
        public void Execute_ShouldTranslateDescriptionUsingYoda_WhenIsLegendary()
        {
            // Arrange 
            var pokemon = _fixture.Build<Pokemon>()
                      .With(p => p.IsLegendary, true)
                      .Create();

            funTranslationsApiClient.Setup(f => f.Translate(It.IsAny<string>(), It.IsAny<TranslationType>())).Returns(() => ServiceResponse<string>.Success(Guid.NewGuid().ToString()));

            // Act
            _sut.Execute(pokemon);

            // Assert
            funTranslationsApiClient.Verify(f => f.Translate(It.IsAny<string>(), It.IsAny<TranslationType>()), Times.Once);
        }

        [Test]
        public void Execute_ShouldReturnPokemonWithStandadDescription_WhenTanslationServiceIsUnavailable()
        {
            // Arrange 
            var pokemon = _fixture.Create<Pokemon>();
            funTranslationsApiClient.Setup(f => f.Translate(It.IsAny<string>(), It.IsAny<TranslationType>())).Throws(new ApplicationException());

            // Act And Assert
            Assert.Throws<ArgumentException>(() => _sut.Execute(pokemon));
            funTranslationsApiClient.Verify(f => f.Translate(pokemon.Description, It.IsAny<TranslationType>()), Times.Once);
        }

        [Test]
        public void Execute_ShakespeareTranslation_ShouldReturnPokemonDescriptionTranslated()
        {
            // Arrange 
            var pokemon = _fixture.Build<Pokemon>()
                                  .With(p => p.IsLegendary, false)
                                  .Create();

            funTranslationsApiClient.Setup(f => f.Translate(It.IsAny<string>(), It.IsAny<TranslationType>())).Returns(() => ServiceResponse<string>.Success(Guid.NewGuid().ToString()));


            // Act
            var translatedPokemon = _sut.Execute(pokemon);

            // Assert
            funTranslationsApiClient.Verify(f => f.Translate(It.IsAny<string>(), It.IsAny<TranslationType>()), Times.Once);
            Assert.That(translatedPokemon.Name, Is.EqualTo(pokemon.Name));
        }

    }
}
