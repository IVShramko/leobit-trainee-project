using Dating.Logic.DTO;
using Dating.Logic.Facades.SearchFacade;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Dating.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchFacade _searchFacade;

        public SearchController(ISearchFacade searchFacade)
        {
            _searchFacade = searchFacade;
        }

        [HttpPost]
        public async Task<IActionResult> Criteria(SearchCriteria criteria)
        {
            ProfileSearchresult result = await _searchFacade.Search(criteria);

            return Ok(result);
        }
    }
}
