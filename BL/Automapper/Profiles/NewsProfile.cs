using AutoMapper;
using DAL;

namespace BL.Automapper.Profiles
{
    public class NewsProfile : Profile
    {
        public NewsProfile()
        {
            CreateMap<News, NewsDTO>().ReverseMap();
        }
    }
}