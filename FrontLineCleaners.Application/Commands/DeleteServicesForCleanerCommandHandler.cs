using FrontLineCleaners.Domain.Entities;
using FrontLineCleaners.Domain.Exceptions;
using FrontLineCleaners.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontLineCleaners.Application.Commands;

public class DeleteServicesForCleanerCommandHandler(
    ILogger<DeleteServicesForCleanerCommandHandler> logger,
    ICleanersRepository cleanersRepository,
    IServicesRepository servicesRepository) : IRequestHandler<DeleteServicesForCleanerCommand>
{
    public async Task Handle(DeleteServicesForCleanerCommand request, CancellationToken cancellationToken)
    {
        logger.LogWarning("Deleting all services for cleaner with Id: {CleanerId}", request.Id);

        var cleaner = await cleanersRepository.GetByIdAsync(request.Id);
        if (cleaner == null)
        {
            throw new NotFoundException(nameof(Cleaner), request.Id.ToString());
        }
        var services = cleaner.Services;
        if (services == null)
        {
            throw new NotFoundException(nameof(Service), request.Id.ToString());
        }
        await servicesRepository.Delete(services);
    }
}
