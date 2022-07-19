using System.Collections.Generic;

namespace Dating.Logic.DTO
{
    public class ProfileSearchresult
    {
        public int ResultsTotal { get; set; }

        public IEnumerable<ProfileListDTO> Profiles { get; set; }
    }
}
