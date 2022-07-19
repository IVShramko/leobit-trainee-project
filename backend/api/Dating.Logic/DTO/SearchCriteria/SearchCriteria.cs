using Dating.Logic.Enums;

namespace Dating.Logic.DTO
{
    public class SearchCriteria
    {
        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public Filters Filter { get; set; }

        public ProfileCriteria Profile { get; set; }
    }
}
