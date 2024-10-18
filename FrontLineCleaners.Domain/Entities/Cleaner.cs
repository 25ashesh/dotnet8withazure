using System.Net;
using System.Net.Sockets;

namespace FrontLineCleaners.Domain.Entities
{
    public class Cleaner
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!; 
        public string Description { get; set; } = default!;
        public string Category { get; set; } = default!;
        public bool HasOwnSupplies { get; set; }

        public string? ContactEmail { get; set; }
        public string? ContactNumber { get; set; }
        public Address? Address { get; set; }
        public List<Service> Services { get; set; } = new();

        public User Owner { get; set; } = default!;
        public string OwnerId { get; set; } = default!;
    }
}
