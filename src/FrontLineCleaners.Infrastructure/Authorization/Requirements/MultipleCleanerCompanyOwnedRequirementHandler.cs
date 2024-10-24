using FrontLineCleaners.Application.User;
using FrontLineCleaners.Domain.Exceptions;
using FrontLineCleaners.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontLineCleaners.Infrastructure.Authorization.Requirements;

public class MultipleCleanerCompanyOwnedRequirementHandler(ILogger<MultipleCleanerCompanyOwnedRequirementHandler> logger,
    IUserContext userContext,
    ICleanersRepository cleanersRepository) : AuthorizationHandler<MultipleCleanerCompanyOwnedRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MultipleCleanerCompanyOwnedRequirement requirement)
    {
        var currentUser = userContext.GetCurrentUser();
        if (currentUser == null) 
        {
            throw new ForbidException();
        }

        logger.LogInformation("User: {Email} - Handling MinimumCleanerCompanyOwnedRequirement",
            currentUser.Email);

        var cleanerCompanies = await cleanersRepository.GetAllAsync();
        var cleanerCompaniesOwned = cleanerCompanies.Count(x => x.OwnerId == currentUser.Id);
        if (cleanerCompaniesOwned >= requirement.MinimumCompanyOwned)
        {
            logger.LogInformation("Authorization based on minimum cleaner company owned succeeded!");
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }
        //return Task.CompletedTask;
    }
}
