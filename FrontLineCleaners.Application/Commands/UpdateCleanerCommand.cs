using MediatR;

namespace FrontLineCleaners.Application.Commands
{
    public class UpdateCleanerCommand: IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public bool HasOwnSupplies { get; set; }
    }
}
