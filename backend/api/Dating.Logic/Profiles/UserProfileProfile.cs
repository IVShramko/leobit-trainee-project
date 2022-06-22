using AutoMapper;
using Dating.Logic.DTO;
using Dating.Logic.Models;

namespace Dating.Logic.Profiles
{
    public class UserProfileProfile : Profile
    {
        public UserProfileProfile()
        {
            CreateMap<UserProfileFullDTO, UserProfileFullDTO>();
        }
    }
}
