using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontLineCleaners.Application.Commands;

public class CreateServiceCommandValidator : AbstractValidator<CreateServiceCommand>
{
    public CreateServiceCommandValidator()
    {
        RuleFor(service => service.Cost)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Cost must be a non-negative number.");

        RuleFor(service => service.TimeInHours)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Time must be at least 1 hour or more.");
    }
}
