using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public PokemonController(IPokeApiClient pokeApiClient)
        {
            _pokeApiClient = pokeApiClient;
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
        public ActionResult Get(string name)
        {
            var pokeApiServiceResult = _pokeApiClient.Get(name);

            if (!pokeApiServiceResult.Succeeded) {
                return NotFound();
            }

            return Ok(pokeApiServiceResult.Content);
        }

    }
}
