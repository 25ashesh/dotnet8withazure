using AutoMapper;
using FrontLineCleaners.Application.Dtos;
using FrontLineCleaners.Domain.Entities;
using FrontLineCleaners.Domain.Exceptions;
using FrontLineCleaners.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FrontLineCleaners.Application.Queries;

public class GetCleanerByIdQueryHandler(ILogger<GetCleanerByIdQueryHandler> logger,
    IMapper mapper,
    ICleanersRepository cleanersRepository) : IRequestHandler<GetCleanerByIdQuery, CleanerDto>
{
    public async Task<CleanerDto> Handle(GetCleanerByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting cleaner {CleanerId}", request.Id);
        var cleaner = await cleanersRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Cleaner), request.Id.ToString()); ;

        var cleanerDto = mapper.Map<CleanerDto>(cleaner);

        return cleanerDto;
    }
}
