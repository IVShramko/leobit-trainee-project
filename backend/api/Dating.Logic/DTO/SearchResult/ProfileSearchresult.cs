using System.Collections.Generic;

namespace Dating.Logic.DTO
{
    public class ProfileSearchresult
    {
        public int ResultsTotal { get; set; }

        public ICollection<ProfileListDTO> Profiles { get; set; }
    }
}
