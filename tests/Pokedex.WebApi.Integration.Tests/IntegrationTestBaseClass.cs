using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;

namespace Pokedex.WebApi.Integration.Tests
{

    public class IntegrationTestBaseClass
    {

        protected HttpClient Client;
        private readonly WebApplicationFactory<Startup> _factory;

        public IntegrationTestBaseClass()
        {
            _factory = new WebApplicationFactory<Startup>();

            Client = _factory.CreateClient();
        }
    }
}
