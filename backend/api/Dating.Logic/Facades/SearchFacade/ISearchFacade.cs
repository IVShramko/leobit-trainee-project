using Dating.Logic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dating.Logic.Facades.SearchFacade
{
    public interface ISearchFacade
    {
        public Task<ICollection<SearchResultDTO>> Search(CriteriaDTO criteria);
    }
}
