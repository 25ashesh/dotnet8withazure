using AutoMapper;
using FrontLineCleaners.Application.User;
using FrontLineCleaners.Domain.Entities;
using FrontLineCleaners.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontLineCleaners.Application.Commands;

public class CreateCleanerCommandHandler (ILogger<CreateCleanerCommandHandler> logger,
    IMapper mapper,
    ICleanersRepository cleanersRepository,
    IUserContext userContext) : IRequestHandler<CreateCleanerCommand, int>
{
    public async Task<int> Handle(CreateCleanerCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        logger.LogInformation("{UserEmail} [{UserId}] Creating a new cleaner {@Cleaner}",
            currentUser.Email,
            currentUser.Id,
            request);

        var cleaner = mapper.Map<Cleaner>(request);
        cleaner.OwnerId = currentUser.Id;
        int id = await cleanersRepository.Create(cleaner);
        return id;
    }
}
