using AutoMapper;
using FrontLineCleaners.Domain.Entities;
using FrontLineCleaners.Domain.Exceptions;
using FrontLineCleaners.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FrontLineCleaners.Application.Commands;

public class UpdateCleanerCommandHandler(ILogger<UpdateCleanerCommandHandler> logger,
    IMapper mapper,
    ICleanersRepository cleanersRepository) : IRequestHandler<UpdateCleanerCommand>
{
    public async Task Handle(UpdateCleanerCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating cleaner with Id: {CleanerId} with {@UpdatedCleaner}", request.Id, request);

        var cleaner = await cleanersRepository.GetByIdAsync(request.Id);
        if (cleaner == null)
        {
            throw new NotFoundException(nameof(Cleaner), request.Id.ToString());
        }

        mapper.Map(request, cleaner);

        //cleaner.Name = request.Name;
        //cleaner.Description = request.Description;
        //cleaner.HasOwnSupplies = request.HasOwnSupplies;
        await cleanersRepository.SaveChanges();
    }
}
