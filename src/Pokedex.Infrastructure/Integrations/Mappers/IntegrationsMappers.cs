using Pokedex.Infraclassure.Integrations.PokeApi.Contracts.Response.v2;

namespace Pokedex.Infrastructure.Integrations.Mappers
{
    public static class IntegrationsMappers
    {
        public static Domain.DomainModel.Pokemon ToPokemon(this PokemonSpeciesResponse pokemonSpeciesResponse)
        {
            return new Domain.DomainModel.Pokemon {
                Description = pokemonSpeciesResponse.FlavorTextEntries[0]?.FlavorText,
                Habitat = pokemonSpeciesResponse.Habitat.Name,
                IsLegendary = pokemonSpeciesResponse.IsLegendary,
                Name = pokemonSpeciesResponse.Name
            };
        }
    }
}
