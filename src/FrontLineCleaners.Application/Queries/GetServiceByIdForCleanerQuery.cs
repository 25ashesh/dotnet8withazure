using FrontLineCleaners.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontLineCleaners.Application.Queries;

public class GetServiceByIdForCleanerQuery(int cleanerId, int serviceId):IRequest<ServiceDto>
{
    public int CleanerId { get; } = cleanerId;
    public int ServiceId { get; } = serviceId;
}
