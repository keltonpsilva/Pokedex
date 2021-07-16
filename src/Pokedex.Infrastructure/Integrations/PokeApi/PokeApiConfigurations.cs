using Microsoft.Extensions.Configuration;
using Pokedex.Infrastructure.Integrations.PokeApi.Interfaces;

namespace Pokedex.Infrastructure.Integrations.PokeApi
{
    public class PokeApiConfigurations : IPokeApiConfigurations
    {
        private readonly IConfiguration _configuration;

        public PokeApiConfigurations(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string BaseUrl => _configuration.GetValue<string>("PokeApiConfigurations:BaseUrl");

    }
}
