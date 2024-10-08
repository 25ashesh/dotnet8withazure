using FluentValidation;
using FrontLineCleaners.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontLineCleaners.Application.Commands;

public class CreateCleanerCommandValidator : AbstractValidator<CreateCleanerCommand>
{
    private readonly List<string> validCategories = ["Complete", "Home", "Business"];
    public CreateCleanerCommandValidator()
    {
        RuleFor(dto => dto.Name)
            .Length(3, 100);

        RuleFor(dto => dto.Description)
            .NotEmpty().WithMessage("Description is required!");

        //RuleFor(dto => dto.Category)
        //    .NotEmpty().WithMessage("Insert a valid category!");

        //RuleFor(dto => dto.Category)
        //.Custom((value, context) =>
        //{
        //    var isValidCategory = validCategories.Contains(value);
        //    if (isValidCategory)
        //    {
        //        context.AddFailure("Category", "Invalid Category. Please choose from the valid categories.");
        //    }
        //});

        RuleFor(dto => dto.Category)
        .Must(category => validCategories.Contains(category))
        .WithMessage("Invalid Category. Please choose from the valid categories.");

        RuleFor(dto => dto.ContactNumber)
            .Length(10).WithMessage("Enter a valid phone number!");

        RuleFor(dto => dto.ContactEmail)
            .EmailAddress().WithMessage("Enter a valid email!");

        RuleFor(dto => dto.PostalCode)
            .Matches(@"^\d{3}-\d{3}$").WithMessage("Please enter a valid postal code (XXX-XXX)!");
    }
}
