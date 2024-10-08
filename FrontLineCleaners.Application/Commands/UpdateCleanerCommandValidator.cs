using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontLineCleaners.Application.Commands;

public class UpdateCleanerCommandValidator : AbstractValidator<UpdateCleanerCommand>
{
    public UpdateCleanerCommandValidator()
    {
        RuleFor(dto => dto.Name)
            .Length(3, 100);
    }
}
