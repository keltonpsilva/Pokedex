using Pokedex.Application.Contracts.Response;
using Pokedex.Infraclassure.Integrations.PokeApi.Contracts.Response.v2;
using Pokedex.Infrastructure.Integrations.Mappers;
using Pokedex.Infrastructure.Integrations.PokeApi.Interfaces;
using RestSharp;

namespace Pokedex.Infrastructure.Integrations.PokeApi
{
    public class PokeApiClient : IntegrationClient, IPokeApiClient
    {
        private readonly IPokeApiConfigurations _pokeApiConfigurations;

        public PokeApiClient(IRestClient restClient,
                       IPokeApiConfigurations pokeApiConfigurations) : base(restClient)
        {
            _pokeApiConfigurations = pokeApiConfigurations;
        }

        public ServiceResponse<Domain.DomainModel.Pokemon> Get(string pokemonName)
        {
            var request = new RestRequest {
                Method = Method.GET,
                Resource = $"/v2/pokemon-species/{pokemonName.ToLower()}",
            };

            try {
                var response = ExecuteRequest<PokemonSpeciesResponse>(_pokeApiConfigurations.BaseUrl, request);

                return ServiceResponse<Domain.DomainModel.Pokemon>.Success(response.ToPokemon());
            }
            catch (System.Exception ex) {

                return ServiceResponse<Domain.DomainModel.Pokemon>.Fail(ex.Message);
            }
        }
    }
}
