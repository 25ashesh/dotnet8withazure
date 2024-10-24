using FrontLineCleaners.Application.User;
using FrontLineCleaners.Domain.Constants;
using FrontLineCleaners.Domain.Entities;
using FrontLineCleaners.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontLineCleaners.Infrastructure.Authorization.Services;

public class CleanerAuthorizationService(ILogger<CleanerAuthorizationService> logger,
    IUserContext userContext) : ICleanerAuthorizationService
{
    public bool Authorize(Cleaner cleaner, ResourceOperation resourceOperation)
    {
        var user = userContext.GetCurrentUser();

        logger.LogInformation("Authorizing user {UserEmail}, to {Operation} for cleaner {CleanerCompanyName}",
            user.Email,
            resourceOperation,
            cleaner.Name);

        if (resourceOperation == ResourceOperation.Read || resourceOperation == ResourceOperation.Create)
        {
            logger.LogInformation("Create/read operation - successful authorization");
            return true;
        }

        if (resourceOperation == ResourceOperation.Delete && user.IsInRole(UserRoles.Admin))
        {
            logger.LogInformation("Admin user, delete operation - successful authorization");
            return true;
        }

        if ((resourceOperation == ResourceOperation.Delete || resourceOperation == ResourceOperation.Update)
            && user.Id == cleaner.OwnerId)
        {
            logger.LogInformation("Cleaner owner - successful authorization");
            return true;
        }

        return false;
    }
}
