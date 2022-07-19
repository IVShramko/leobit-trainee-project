namespace Dating.Logic.DTO
{
    public class ProfileCriteria
    {
        public bool Gender { get; set; }

        public int? MinAge { get; set; }

        public int? MaxAge { get; set; }

        public string Region { get; set; }

        public string Town { get; set; }
    }
}
