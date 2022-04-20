using AutoMapper;
using BlogsApi.Dtos;
using BlogsApi.Helpers;

namespace BlogsApi.Configuration
{
    public class MapperIntializer : Profile
    {
        public MapperIntializer()
        {
            CreateMap<AppUser,UserDTOS>().ReverseMap();
        }
    }
}
