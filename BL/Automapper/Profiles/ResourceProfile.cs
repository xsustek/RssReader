using AutoMapper;
using BL.DTOs;
using DAL;

namespace BL.Automapper.Profiles
{
    public class ResourceProfile : Profile
    {
        public ResourceProfile()
        {
            CreateMap<Resource, ResourceDTO>().ReverseMap();
        }
    }
}