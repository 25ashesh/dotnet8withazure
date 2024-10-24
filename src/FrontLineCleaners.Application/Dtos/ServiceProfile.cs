using AutoMapper;
using FrontLineCleaners.Application.Commands;
using FrontLineCleaners.Domain.Entities;

namespace FrontLineCleaners.Application.Dtos;

public class ServiceProfile:Profile
{
    public ServiceProfile()
    {
        CreateMap<CreateServiceCommand, Service>();
        CreateMap<Service, ServiceDto>();
    }
}
