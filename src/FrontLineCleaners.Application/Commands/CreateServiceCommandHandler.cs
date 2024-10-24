using AutoMapper;
using FrontLineCleaners.Domain.Entities;
using FrontLineCleaners.Domain.Exceptions;
using FrontLineCleaners.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontLineCleaners.Application.Commands;

public class CreateServiceCommandHandler(ILogger<CreateServiceCommandHandler> logger,
    ICleanersRepository cleanersRepository,
    IServicesRepository servicesRepository,
    IMapper mapper) : IRequestHandler<CreateServiceCommand, int>
{
    public async Task<int> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new service: {@DishRequest}", request);
        var cleaner = await cleanersRepository.GetByIdAsync(request.CleanerId);
        if (cleaner == null) { throw new NotFoundException(nameof(Cleaner), request.CleanerId.ToString()); }

        var service = mapper.Map<Service>(request);
        return await servicesRepository.Create(service);
    }
}
