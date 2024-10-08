using FrontLineCleaners.Application.Dtos;
using MediatR;

namespace FrontLineCleaners.Application.Queries;

public class GetAllCleanersQuery:IRequest<IEnumerable<CleanerDto>>
{
}
