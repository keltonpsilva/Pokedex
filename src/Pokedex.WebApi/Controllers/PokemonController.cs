using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pokedex.Application.Interfaces;
using Pokedex.Domain.DomainModel;
using Pokedex.Infrastructure.Integrations.PokeApi.Interfaces;
using System.Net.Mime;

namespace Pokedex.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly IPokeApiClient _pokeApiClient;
        private readonly ITranslationCommand _translationCommand;

        public PokemonController(
            IPokeApiClient pokeApiClient,
            ITranslationCommand translationCommand)
        {
            _pokeApiClient = pokeApiClient;
            _translationCommand = translationCommand;
        }

        /// <summary>
        /// Get Pokemon information
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        /// GET /api/pokemon/ditto
        /// 
        /// </remarks>
        /// <param name="name"></param>
        /// <returns>Return basic Pokemon information</returns>
        [HttpGet]
        [Route("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Pokemon))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ResponseCache(Duration = 3600)]
        public ActionResult Get(string name)
        {
            var pokeApiServiceResult = _pokeApiClient.Get(name);

            if (!pokeApiServiceResult.Succeeded) {
                return NotFound();
            }

            return Ok(pokeApiServiceResult.Content);
        }

        /// <summary>
        /// Get Pokemon with fun translation if possible
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        /// GET /api/pokemon/translate/ditto
        /// 
        /// </remarks>
        /// <param name="name">Pokemon name</param>
        /// <returns>Return basic Pokemon information but with a "fun" translation of the Pokemon description </returns>
        [HttpGet]
        [Route("translate/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Pokemon))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ResponseCache(Duration = 3600)]
        public IActionResult Translate(string name)
        {
            var pokeApiServiceResult = _pokeApiClient.Get(name);

            if (!pokeApiServiceResult.Succeeded) {
                return NotFound();
            }

            var resposeModel = _translationCommand.Execute(pokeApiServiceResult.Content);

            return Ok(resposeModel);
        }
    }
}
