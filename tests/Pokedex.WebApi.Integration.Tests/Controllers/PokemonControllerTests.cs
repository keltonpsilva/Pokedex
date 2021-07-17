using Newtonsoft.Json;
using NUnit.Framework;
using Pokedex.Domain.DomainModel;
using System.Net;
using System.Threading.Tasks;

namespace Pokedex.WebApi.Integration.Tests.Controllers
{
    [TestFixture]
    public class PokemonControllerTests : IntegrationTestBaseClass
    {
        [Test]
        public async Task Get_ValidPokemonName_ShouldReturnOkResponseWithPokemonData()
        {
            // Arrange
            var pokemonName = "ditto";

            // Act
            var response = await Client.GetAsync($"api/pokemon/{pokemonName}");
            var responseContent = JsonConvert.DeserializeObject<Pokemon>(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(responseContent, Is.Not.Null);
            Assert.That(responseContent.Name, Is.EqualTo(pokemonName));
        }

        [Test]
        [Ignore(" For public API calls this is 60 API calls a day with distribution of 5 calls an hour")]
        public async Task Translate_ValidPokemonName_ShouldReturnOkResponseWithPokemonDataAndDescriptionTranslated()
        {
            // Arrange
            var pokemonName = "charizard";
            var expectedTranslation = "Spits fire yond is hot enow to melt boulders. Known to cause forest fires unintentionally.";

            // Act
            var response = await Client.GetAsync($"api/pokemon/translate/{pokemonName}");
            var responseContent = JsonConvert.DeserializeObject<Pokemon>(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(responseContent, Is.Not.Null);
            Assert.That(responseContent.Description, Is.EqualTo(expectedTranslation));
        }
    }
}
