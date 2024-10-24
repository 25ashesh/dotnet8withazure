using FrontLineCleaners.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontLineCleaners.Application.Queries;

public class GetServicesForCleanerQuery(int cleanerId): IRequest<IEnumerable<ServiceDto>>
{
    public int CleanerId { get; } = cleanerId;
}
