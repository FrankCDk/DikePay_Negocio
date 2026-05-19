using AutoMapper;
using DikePay.Modules.Configuration.Domain;
using DikePay.Modules.Configuration.Shared.Contracts.v1.Commands;

namespace DikePay.Modules.Configuration.Application.Features.v1.Versions.Mappers
{
    public class CreateVersionCommandProfile : Profile
    {
        public CreateVersionCommandProfile()
        {

            CreateMap<CreateVersionCommand, AppVersion>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
            //.ForMember(dest => dest.Platform, opt => opt.MapFrom(src => src.Platform))
            //.ForMember(dest => dest.VersionNumber, opt => opt.MapFrom(src => src.VersionNumber))
            //.ForMember(dest => dest.BuildNumber, opt => opt.MapFrom(src => src.BuildNumber))
        }
    }
}
