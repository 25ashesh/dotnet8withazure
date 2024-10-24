using AutoMapper;
using FrontLineCleaners.Application.Dtos;
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

namespace FrontLineCleaners.Application.Queries;

public class GetServiceByIdForCleanerQueryHandler(
    ILogger<GetServiceByIdForCleanerQueryHandler> logger,
    ICleanersRepository cleanersRepository,
    IMapper mapper) : IRequestHandler<GetServiceByIdForCleanerQuery, ServiceDto>
{
    public async Task<ServiceDto> Handle(GetServiceByIdForCleanerQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Retrieving service : {ServiceId}, for cleaner with id: {CleanerId}",
            request.ServiceId,
            request.CleanerId);

        var cleaner = await cleanersRepository.GetByIdAsync(request.CleanerId);
        if (cleaner == null) throw new NotFoundException(nameof(Cleaner), request.CleanerId.ToString());

        var service = cleaner.Services.FirstOrDefault(d => d.Id == request.ServiceId);        
        if (service == null) throw new NotFoundException(nameof(Service), request.ServiceId.ToString());

        var result = mapper.Map<ServiceDto>(service);
        return result;
    }
}
