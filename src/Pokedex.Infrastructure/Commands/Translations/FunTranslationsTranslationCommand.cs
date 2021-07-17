using Microsoft.Extensions.Logging;
using Pokedex.Application.Interfaces;
using Pokedex.Domain.Common.Enums;
using Pokedex.Domain.DomainModel;
using Pokedex.Infrastructure.Integrations.FunTranslationsApi.Interfaces;
using System;

namespace Pokedex.Infrastructure.Commands.Translations
{
    public class FunTranslationsTranslationCommand : ITranslationCommand
    {
        private readonly IFunTranslationsApiClient _funTranslationsApiClient;
        private readonly ILogger<FunTranslationsTranslationCommand> _logger;

        private const string caveHabitat = "cave";

        public FunTranslationsTranslationCommand(IFunTranslationsApiClient funTranslationsApiClient,
                                                 ILogger<FunTranslationsTranslationCommand> logger)
        {
            _funTranslationsApiClient = funTranslationsApiClient;
            _logger = logger;
        }

        public Pokemon Execute(Pokemon pokemon)
        {
            try {
                var translationType = (pokemon.Habitat == caveHabitat || pokemon.IsLegendary) ? TranslationType.Yoda : TranslationType.Shakespeare;

                var translateServiceResult = _funTranslationsApiClient.Translate(pokemon.Description, translationType);

                if (translateServiceResult.Succeeded && !string.IsNullOrEmpty(translateServiceResult.Content)) {
                    pokemon.Description = translateServiceResult.Content;
                }
            }
            catch (Exception ex) {
                _logger.LogError($"Failed translating {pokemon.Name} : {ex.Message}");
                throw new ArgumentException($"Failed translating {pokemon.Name}", nameof(pokemon.Name));
            }

            return pokemon;
        }
    }
}
