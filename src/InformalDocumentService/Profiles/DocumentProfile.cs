using AbonentService;
using AutoMapper;
using InformalDocumentService.Data.Models;
using InformalDocumentService.Dtos;

namespace InformalDocumentService.Profiles
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            // Source -> Target
            CreateMap<Document, DocumentReadDto>();
            CreateMap<DocumentCreateDto, Document>();
            CreateMap<AbonentCreateDto, Abonent>();
            CreateMap<Abonent, AbonentReadDto>();
            CreateMap<AbonentCreatedDto, Abonent>()
                .ForMember(dest =>  dest.ExternalId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<GrpcAbonentModel, Abonent>()
                .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.AbonentId))
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
       
    }
}
