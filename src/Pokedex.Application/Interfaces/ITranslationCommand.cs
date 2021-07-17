using Pokedex.Domain.DomainModel;

namespace Pokedex.Application.Interfaces
{
    public interface ITranslationCommand
    {
        Pokemon Execute(Pokemon pokemon);
    }
}
