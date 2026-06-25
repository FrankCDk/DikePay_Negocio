using AutoMapper;
using DikePay.Modules.Configuracion.Application.Contracts.v1.Commands;
using DikePay.Modules.Configuracion.Domain;

namespace DikePay.Modules.Configuracion.Application.Features.v1.Versions.Mappers
{
    public class CreaRVersionCommandProfile : Profile
    {
        public CreaRVersionCommandProfile()
        {

            CreateMap<CrearVersionCommand, VersionApp>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
            //.ForMember(dest => dest.Platform, opt => opt.MapFrom(src => src.Platform))
            //.ForMember(dest => dest.VersionNumber, opt => opt.MapFrom(src => src.VersionNumber))
            //.ForMember(dest => dest.BuildNumber, opt => opt.MapFrom(src => src.BuildNumber))
        }
    }
}
