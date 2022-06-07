using Dating.Logic.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Dating.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        [HttpPost]
        public IActionResult Criteria(CriteriaDTO criteria)
        {
            return Ok();
        }
    }
}
