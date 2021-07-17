using Pokedex.Application.Contracts.Response;
using Pokedex.Domain.Common.Enums;

namespace Pokedex.Infrastructure.Integrations.FunTranslationsApi.Interfaces
{
    public interface IFunTranslationsApiClient
    {
        ServiceResponse<string> Translate(string text, TranslationType translationType);
    }
}
