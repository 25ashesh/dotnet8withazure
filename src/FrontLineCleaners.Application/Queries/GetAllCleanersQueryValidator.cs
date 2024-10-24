using FluentValidation;
using FrontLineCleaners.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontLineCleaners.Application.Queries;

public class GetAllCleanersQueryValidator : AbstractValidator<GetAllCleanersQuery>
{
    private int[] allowedPageSizes = [5, 10, 15, 30];

    private string[] allowedSortByColumnNames = [nameof(CleanerDto.Name),
        nameof(CleanerDto.Category),
        nameof(CleanerDto.Description)];

    public GetAllCleanersQueryValidator()
    {
        RuleFor(r => r.PageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(r => r.PageSize)
            .Must(value => allowedPageSizes.Contains(value))
            .WithMessage($"Page size must be in [{string.Join(",", allowedPageSizes)}]");

        RuleFor(r => r.SortBy)
            .Must(value => allowedSortByColumnNames.Contains(value))
            .When(q => q.SortBy != null)
            .WithMessage($"Sort by is optional, or must be in [{string.Join(",", allowedSortByColumnNames)}]");
    }
}
