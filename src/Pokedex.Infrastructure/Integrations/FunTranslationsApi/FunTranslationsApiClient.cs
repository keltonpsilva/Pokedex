using Pokedex.Application.Contracts.Response;
using Pokedex.Domain.Common.Enums;
using Pokedex.Infrastructure.Integrations.FunTranslationsApi.Contracts.Request.v1;
using Pokedex.Infrastructure.Integrations.FunTranslationsApi.Contracts.Response.v1;
using Pokedex.Infrastructure.Integrations.FunTranslationsApi.Interfaces;
using RestSharp;

namespace Pokedex.Infrastructure.Integrations.FunTranslationsApi
{
    public class FunTranslationsApiClient : IntegrationClient, IFunTranslationsApiClient
    {
        private readonly IFunTranslationsApiConfigurations _funTranslationsApiConfigurations;

        public FunTranslationsApiClient(
            IRestClient restClient,
            IFunTranslationsApiConfigurations funTranslationsApiConfigurations) : base(restClient)
        {
            _funTranslationsApiConfigurations = funTranslationsApiConfigurations;
        }

        public ServiceResponse<string> Translate(string text, TranslationType translationType)
        {
            var request = new RestRequest {
                Method = Method.POST,
                Resource = GetResource(translationType)
            };

            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new FunTranslationsRequest { Text = text });

            try {

                var response = ExecuteRequest<FunTranslationsResponse>(_funTranslationsApiConfigurations.BaseUrl, request).Contents?.Translated;
                return ServiceResponse<string>.Success(response);
            }
            catch (System.Exception ex) {

                return ServiceResponse<string>.Fail(ex.Message);
            }
        }

        private string GetResource(TranslationType translationType)
        {
            if (translationType == TranslationType.Yoda) {
                return "/translate/yoda";
            } else {
                return "/translate/shakespeare";
            }
        }

    }
}
