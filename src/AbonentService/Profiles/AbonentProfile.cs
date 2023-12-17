using AutoMapper;
using AbonentService.Data.Models;
using AbonentService.Dtos;

namespace AbonentService.Profiles
{
    public class AbonentProfile : Profile
    {
        public AbonentProfile()
        {
            // Source -> Target
            CreateMap<Abonent, AbonentReadDto>();
            CreateMap<AbonentCreateDto, Abonent>();
            CreateMap<AbonentReadDto, AbonentCreatedDto>();
            CreateMap<Abonent,GrpcAbonentModel>()
                .ForMember(dest => dest.AbonentId, opt => opt.MapFrom(src => src.Id));
                
        }
       
    }
}
