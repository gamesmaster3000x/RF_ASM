using BerryMVC.Data;
using Microsoft.AspNetCore.Mvc;

namespace BerryMVC.Controllers
{
    public class ApiController : Controller
    {
        private readonly ILogger<HomeController> LOGGER;
        private readonly BerryMVCContext _berryMVCContext;

        public ApiController (ILogger<HomeController> logger, BerryMVCContext berryMVCContext)
        {
            LOGGER = logger;
            _berryMVCContext = berryMVCContext;
        }

        [AcceptVerbs("Get")]
        public IActionResult GetBerry (string path)
        {
            return Ok("Lol get got");
        }

        [AcceptVerbs("Post")]
        public IActionResult Post ([FromBody] string body)
        {
            return Ok("Lol get posted");
        }
    }
}