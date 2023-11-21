using AutoMapper;
using ProjectWebAPI.DTO;
using ProjectWebAPI.Entity;

namespace ProjectWebAPI.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
        }
    }

}