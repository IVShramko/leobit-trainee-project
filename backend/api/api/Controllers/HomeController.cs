using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
