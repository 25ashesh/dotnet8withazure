using FrontLineCleaners.Application.Common;
using FrontLineCleaners.Application.Dtos;
using FrontLineCleaners.Domain.Constants;
using MediatR;

namespace FrontLineCleaners.Application.Queries;

public class GetAllCleanersQuery:IRequest<PagedResult<CleanerDto>>
{
    public string? SearchPhrase { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? SortBy { get; set; }
    public SortDirection SortDirection { get; set; }
}
