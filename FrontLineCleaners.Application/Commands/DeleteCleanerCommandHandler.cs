using AutoMapper;
using FrontLineCleaners.Domain.Entities;
using FrontLineCleaners.Domain.Exceptions;
using FrontLineCleaners.Domain.Interfaces;
using FrontLineCleaners.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FrontLineCleaners.Application.Commands;
public class DeleteCleanerCommandHandler(ILogger<DeleteCleanerCommandHandler> logger,
    ICleanersRepository cleanersRepository,
    ICleanerAuthorizationService cleanerAuthorizationService) : IRequestHandler<DeleteCleanerCommand>
{
    public async Task Handle(DeleteCleanerCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting cleaner with Id: {CleanerId}",request.Id);

        var cleaner = await cleanersRepository.GetByIdAsync(request.Id);
        if (cleaner == null ) 
        { 
            throw new NotFoundException(nameof(Cleaner), request.Id.ToString()); 
        }

        if (!cleanerAuthorizationService.Authorize(cleaner, Domain.Constants.ResourceOperation.Delete)) 
        {
            throw new ForbidException();
        }

        await cleanersRepository.Delete(cleaner);
    }
}
