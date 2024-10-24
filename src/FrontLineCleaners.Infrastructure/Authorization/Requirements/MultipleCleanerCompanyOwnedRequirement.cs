using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontLineCleaners.Infrastructure.Authorization.Requirements;

public class MultipleCleanerCompanyOwnedRequirement(int minimumCleanerCompanyOwned):IAuthorizationRequirement
{
    public int MinimumCompanyOwned { get; } = minimumCleanerCompanyOwned;
}
