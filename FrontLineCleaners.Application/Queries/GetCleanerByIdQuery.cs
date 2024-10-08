using FrontLineCleaners.Application.Dtos;
using MediatR;

namespace FrontLineCleaners.Application.Queries
{
    public class GetCleanerByIdQuery(int id) : IRequest<CleanerDto?>
    {
        public int Id { get; } = id;
    }
}
