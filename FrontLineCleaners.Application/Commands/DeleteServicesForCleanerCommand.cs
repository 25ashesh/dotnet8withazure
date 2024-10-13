using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontLineCleaners.Application.Commands;

public class DeleteServicesForCleanerCommand(int cleanerId) : IRequest
{
    public int Id { get; } = cleanerId;
}
