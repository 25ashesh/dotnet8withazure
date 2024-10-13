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

public class GetServicesForCleanerQueryHandler(ILogger<GetServicesForCleanerQueryHandler> logger,
    ICleanersRepository cleanersRepository,
    IMapper mapper) : IRequestHandler<GetServicesForCleanerQuery, IEnumerable<ServiceDto>>
{
    public async Task<IEnumerable<ServiceDto>> Handle(GetServicesForCleanerQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Retrieving services for cleaner with id: {CleanerId}", request.CleanerId);
        var cleaner = await cleanersRepository.GetByIdAsync(request.CleanerId);

        if (cleaner == null) throw new NotFoundException(nameof(Cleaner), request.CleanerId.ToString());

        var results = mapper.Map<IEnumerable<ServiceDto>>(cleaner.Services);
        return results;
    }
}
