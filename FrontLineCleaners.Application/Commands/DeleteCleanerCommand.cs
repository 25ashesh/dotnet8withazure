using MediatR;

namespace FrontLineCleaners.Application.Commands;

public class DeleteCleanerCommand(int id):IRequest
{
    public int Id { get; } = id;
}
