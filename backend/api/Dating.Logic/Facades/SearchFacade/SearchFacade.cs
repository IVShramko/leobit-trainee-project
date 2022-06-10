using Dating.Logic.DTO;
using Dating.Logic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<SearchResultDTO> Search(CriteriaDTO criteria)
        {
            return await _repository.GetProfilesOnCriteriaAsync(criteria);
        }
    }
}
