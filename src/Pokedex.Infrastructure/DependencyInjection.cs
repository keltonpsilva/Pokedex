using Microsoft.Extensions.DependencyInjection;
using Pokedex.Infrastructure.Integrations.PokeApi;
using Pokedex.Infrastructure.Integrations.PokeApi.Interfaces;
using RestSharp;

namespace Pokedex.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            AddIoC(services);

            services.AddSingleton<IRestClient, RestClient>();

            return services;
        }

        private static void AddIoC(IServiceCollection services)
        {
            services.AddSingleton<IPokeApiConfigurations, PokeApiConfigurations>();
            services.AddSingleton<IPokeApiClient, PokeApiClient>();
        }
    }
}
