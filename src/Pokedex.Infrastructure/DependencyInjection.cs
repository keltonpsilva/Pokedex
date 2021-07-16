using Microsoft.Extensions.DependencyInjection;
using Pokedex.Application.Interfaces;
using Pokedex.Infrastructure.Commands.Translations;
using Pokedex.Infrastructure.Integrations.FunTranslationsApi;
using Pokedex.Infrastructure.Integrations.FunTranslationsApi.Interfaces;
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
            AddCommand(services);

            services.AddSingleton<IRestClient, RestClient>();

            return services;
        }

        private static void AddIoC(IServiceCollection services)
        {
            services.AddSingleton<IPokeApiConfigurations, PokeApiConfigurations>();
            services.AddSingleton<IPokeApiClient, PokeApiClient>();

            services.AddSingleton<IFunTranslationsApiConfigurations, FunTranslationsApiConfigurations>();
            services.AddSingleton<IFunTranslationsApiClient, FunTranslationsApiClient>();
        }

        private static void AddCommand(IServiceCollection services)
        {
            services.AddSingleton<ITranslationCommand, FunTranslationsTranslationCommand>();
        }
    }
}
