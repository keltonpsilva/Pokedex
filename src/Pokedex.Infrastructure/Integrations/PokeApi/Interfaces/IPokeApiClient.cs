using Pokedex.Application.Contracts.Response;

namespace Pokedex.Infrastructure.Integrations.PokeApi.Interfaces
{
    public interface IPokeApiClient
    {
        ServiceResponse<Domain.DomainModel.Pokemon> Get(string pokemonName);
    }
}
