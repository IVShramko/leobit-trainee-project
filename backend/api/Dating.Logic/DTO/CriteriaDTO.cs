namespace Dating.Logic.DTO
{
    public class CriteriaDTO
    {
        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public ProfileCriteria Profile { get; set; }
    }

    public class ProfileCriteria
    {
        public bool Gender { get; set; }

        public int? MinAge { get; set; }

        public int? MaxAge { get; set; }

        public string Region { get; set; }

        public string Town { get; set; }
    }
}
