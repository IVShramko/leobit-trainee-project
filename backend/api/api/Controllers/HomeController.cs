using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Dating.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : Controller
    {
        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }

        [Authorize]
        [HttpGet]
        public IActionResult Profile()
        {
            return RedirectToRoute(
                Url.Link("api", new { controller = "profile", action = "main" }));
        }
    }
}
