using System.Collections.Generic;

namespace Dating.Logic.DTO
{
    public class SearchResultDTO
    {
        public int ResultsTotal { get; set; }

        public IEnumerable<SearchResultProfile> Profiles { get; set; }
    }

    public class SearchResultProfile
    {
        public int Age { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

    }
}
