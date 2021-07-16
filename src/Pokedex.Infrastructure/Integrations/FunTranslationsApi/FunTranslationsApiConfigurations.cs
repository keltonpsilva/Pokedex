using Microsoft.Extensions.Configuration;
using Pokedex.Infrastructure.Integrations.FunTranslationsApi.Interfaces;

namespace Pokedex.Infrastructure.Integrations.FunTranslationsApi
{
    public class FunTranslationsApiConfigurations : IFunTranslationsApiConfigurations
    {
        private readonly IConfiguration _configuration;

        public FunTranslationsApiConfigurations(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string BaseUrl => _configuration.GetValue<string>("FunTranslationsApiConfigurations:BaseUrl");
    }
}
