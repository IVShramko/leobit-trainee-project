using Dating.Logic.DTO;
using System.Threading.Tasks;

namespace Dating.Logic.Facades.SearchFacade
{
    public interface ISearchFacade
    {
        public Task<SearchResultDTO> Search(CriteriaDTO criteria);
    }
}
