using AutoMapper;
using FrontLineCleaners.Application.Dtos;
using FrontLineCleaners.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FrontLineCleaners.Application.Queries;

public class GetAllCleanersQueryHandler(ILogger<GetAllCleanersQueryHandler> logger,
    IMapper mapper,
    ICleanersRepository cleanersRepository) : IRequestHandler<GetAllCleanersQuery, IEnumerable<CleanerDto>>
{
    public async Task<IEnumerable<CleanerDto>> Handle(GetAllCleanersQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all cleaners");
        var cleaners = await cleanersRepository.GetAllAsync();

        var cleanersDtos = mapper.Map<IEnumerable<CleanerDto>>(cleaners);

        return cleanersDtos!;
    }
}
