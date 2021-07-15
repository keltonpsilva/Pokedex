using Microsoft.AspNetCore.Mvc;

namespace Pokedex.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public ActionResult Index()
        {
            return Ok();
        }
    }
}
