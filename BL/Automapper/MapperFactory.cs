using AutoMapper;
using BL.Automapper.Profiles;

namespace BL.Automapper
{
    public class MapperFactory
    {
        public static IMapper Mapper => new Mapper(new MapperConfiguration(c =>
        {
            c.AddProfile<NewsProfile>();
            c.AddProfile<ResourceProfile>();
        }));
    }
}