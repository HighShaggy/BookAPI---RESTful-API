using AutoMapper;
using BookApi.Data.Models;

namespace BookApi.Data.Mappings;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<Book, BookDto>().ReverseMap();
    }
}