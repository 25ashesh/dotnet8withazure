namespace FrontLineCleaners.Application.Dtos;

public class CreateCleanerDto
{
    //[StringLength(100, MinimumLength = 3)]
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;

    //[Required(ErrorMessage = "Insert a valid category!")]
    public string Category { get; set; } = default!;
    public bool HasOwnSupplies { get; set; }

    //[EmailAddress(ErrorMessage = "Please provide a valid email address!")]
    public string? ContactEmail { get; set; }

    //[Phone(ErrorMessage = "Please provide a valid phone number!")]
    public string? ContactNumber { get; set; }

    public string? City { get; set; }
    public string? Street { get; set; }

    //[RegularExpression(@"^\d{3}-\d{3}$", ErrorMessage = "Please enter a valid postal code (XXX-XXX)!")]
    public string? PostalCode { get; set; }
}
