using AutoMapper;
using BlogsApi.Dtos;
using BlogsApi.Helpers;
using BlogsApi.Model;

namespace BlogsApi.Configuration
{
    public class MapperIntializer : Profile
    {
        public MapperIntializer()
        {
            CreateMap<AppUser,UserDTOS>().ReverseMap();
            CreateMap<Category, CategoryDTOS>().ReverseMap();
            CreateMap<Category, ShowCategoryDTOS>().ReverseMap();
        }
    }
}
