using AutoMapper;
using TechLibrary.Contracts.Requests;
using TechLibrary.Contracts.Responses;
using TechLibrary.Domain;

namespace TechLibrary.MappingProfiles
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<Book, BookEditResponse>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.BookId.Value));
            CreateMap<Book, BookSearchResponse>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.BookId.Value))
                .ForMember(x => x.PublishedDateString, opt => opt.MapFrom(src => src.PublishedDate));
            CreateMap<Book, BookRequest>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.BookId.Value));

            CreateMap<BookEditResponse, Book>()
                .ForMember(x => x.BookId, opt => opt.MapFrom(src => src.Id));
            CreateMap<BookEditResponse, BookSearchResponse>().ForMember(x => x.PublishedDateString, opt => opt.MapFrom(src => src.PublishedDate));
            CreateMap<BookEditResponse, BookRequest>();

            CreateMap<BookSearchResponse, Book>()
                .ForMember(x => x.PublishedDate, opt => opt.MapFrom(src => src.PublishedDateString))
                .ForMember(x => x.BookId, opt => opt.MapFrom(src => src.Id));
            CreateMap<BookSearchResponse, BookEditResponse>().ForMember(x => x.PublishedDate, opt => opt.MapFrom(src => src.PublishedDateString));
            CreateMap<BookSearchResponse, BookRequest>().ForMember(x => x.PublishedDate, opt => opt.MapFrom(src => src.PublishedDateString));

            CreateMap<BookRequest, Book>()
                .ForMember(x => x.BookId, opt => opt.MapFrom(src => src.Id));
            CreateMap<BookRequest, BookEditResponse>().ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id.Value));
            CreateMap<BookRequest, BookSearchResponse>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id.Value))
                .ForMember(x => x.PublishedDateString, opt => opt.MapFrom(src => src.PublishedDate));

            CreateMap<CategorySearchResponse, Category>();
            CreateMap<Category, CategorySearchResponse>();
        }
    }
}