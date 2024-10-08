using AutoMapper;
using FrontLineCleaners.Application.Commands;
using FrontLineCleaners.Domain.Entities;

namespace FrontLineCleaners.Application.Dtos;

public class CleanersProfile : Profile
{
    public CleanersProfile()
    {
        CreateMap<UpdateCleanerCommand, Cleaner>();

        CreateMap<CreateCleanerCommand, Cleaner>()
            .ForMember(d => d.Address, opt => opt.MapFrom(
                src => new Address
                {
                    City = src.City,
                    PostalCode = src.PostalCode,
                    Street = src.Street
                }));

        CreateMap<Cleaner, CleanerDto>()
            .ForMember(d => d.City,
                opt => opt.MapFrom(src => src.Address == null ? null : src.Address.City))
            .ForMember(d => d.PostalCode,
                opt => opt.MapFrom(src => src.Address == null ? null : src.Address.PostalCode))
            .ForMember(d => d.Street,
                opt => opt.MapFrom(src => src.Address == null ? null : src.Address.Street))
            .ForMember(d => d.Services, opt => opt.MapFrom(src => src.Services));
    }
}
