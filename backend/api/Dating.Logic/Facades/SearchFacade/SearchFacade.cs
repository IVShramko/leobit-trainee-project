using Dating.Logic.DTO;
using Dating.Logic.Repositories;
using System.Threading.Tasks;

namespace Dating.Logic.Facades.SearchFacade
{
    public class SearchFacade : ISearchFacade
    {
        private readonly IUserProfileRepository _repository;

        public SearchFacade(IUserProfileRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProfileSearchresult> Search(SearchCriteria criteria)
        {
            return await _repository.GetProfilesOnCriteriaAsync(criteria);
        }
    }
}
