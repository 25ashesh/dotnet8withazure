namespace FrontLineCleaners.Domain.Entities;
public class Service
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Cost { get; set; }
    public int? TimeInHours { get; set; }
    public int CleanerId { get; set; }
}
