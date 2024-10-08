using AutoMapper;
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
    ICleanersRepository cleanersRepository) : IRequestHandler<CreateCleanerCommand, int>
{
    public async Task<int> Handle(CreateCleanerCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating a new cleaner {@Cleaner}",request);
        var cleaner = mapper.Map<Cleaner>(request);
        int id = await cleanersRepository.Create(cleaner);
        return id;
    }
}
