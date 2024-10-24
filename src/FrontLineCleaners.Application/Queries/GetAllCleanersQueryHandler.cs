using AutoMapper;
using FrontLineCleaners.Application.Common;
using FrontLineCleaners.Application.Dtos;
using FrontLineCleaners.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FrontLineCleaners.Application.Queries;

public class GetAllCleanersQueryHandler(ILogger<GetAllCleanersQueryHandler> logger,
    IMapper mapper,
    ICleanersRepository cleanersRepository) : IRequestHandler<GetAllCleanersQuery, PagedResult<CleanerDto>>
{
    public async Task<PagedResult<CleanerDto>> Handle(GetAllCleanersQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all cleaners");
        var (cleaners, totalCount) = await cleanersRepository.GetAllMatchingAsync(request.SearchPhrase, 
            request.PageSize, 
            request.PageNumber,
            request.SortBy,
            request.SortDirection);

        var cleanersDtos = mapper.Map<IEnumerable<CleanerDto>>(cleaners);
        var result = new PagedResult<CleanerDto>(cleanersDtos, totalCount, request.PageSize, request.PageNumber);
        return result;
    }
}
