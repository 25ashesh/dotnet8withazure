using AutoMapper;
using FrontLineCleaners.Domain.Entities;

namespace FrontLineCleaners.Application.Dtos;

public class ServiceProfile:Profile
{
    public ServiceProfile()
    {
        CreateMap<Service, ServiceDto>();
    }
}
