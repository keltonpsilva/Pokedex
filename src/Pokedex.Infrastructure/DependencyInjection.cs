using Microsoft.Extensions.DependencyInjection;
using RestSharp;

namespace Pokedex.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IRestClient, RestClient>();

            return services;
        }
    }
}
