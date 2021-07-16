using RestSharp;

namespace Pokedex.Infrastructure.Integrations.Interfaces
{
    public interface IIntegrationClient
    {
        T ExecuteRequest<T>(string baseUrl, IRestRequest request);
    }
}
