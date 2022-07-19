using Dating.Logic.DTO;
using System.Threading.Tasks;

namespace Dating.Logic.Facades.SearchFacade
{
    public interface ISearchFacade
    {
        Task<ProfileSearchresult> Search(SearchCriteria criteria);
    }
}
