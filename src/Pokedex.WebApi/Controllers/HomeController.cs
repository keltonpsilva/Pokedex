using Microsoft.AspNetCore.Mvc;
using Pokedex.WebApi.Models;

namespace Pokedex.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public ActionResult Index()
        {
            var response = new HomeResponseModel { Application = "Pokedex - Web Api", Documentation = $"{Request.Scheme}://{Request.Host}/docs" };

            return Ok(response);
        }
    }
}
