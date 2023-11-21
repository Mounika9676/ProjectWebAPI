using AutoMapper;
using ProjectWebAPI.DTO;
using ProjectWebAPI.Entity;

namespace ProjectWebAPI.Profiles
{
    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {

            CreateMap<Organization, OrganizationDTO>();

            CreateMap<OrganizationDTO, Organization>();
                

        }
    }
}

