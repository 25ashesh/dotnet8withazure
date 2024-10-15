using FrontLineCleaners.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FrontLineCleaners.Infrastructure.Authorization;

public class FrontLineUserClaimsPrincipalFactory(UserManager<User> userManager,
    RoleManager<IdentityRole> roleManager,
    IOptions<IdentityOptions> options)
    : UserClaimsPrincipalFactory<User, IdentityRole>(userManager, roleManager, options)
{
    public override async Task<ClaimsPrincipal> CreateAsync(User user)
    {
        var id = await GenerateClaimsAsync(user);

        if (user.Nationality !=null) 
        {
            id.AddClaim(new Claim(Constants.AppClaimTypes.Nationality, user.Nationality));
        }

        if (user.DateOfBirth != null)
        {
            id.AddClaim(new Claim(Constants.AppClaimTypes.DateOfBirth, user.DateOfBirth.Value.ToString("yyyy-MM-dd")));
        }
        return new ClaimsPrincipal(id);
    }
}
/*
 * But if our logic to extend the claims principle will be more complex, 
 * we could just simply inject any kind of service or a dbContext to this user claims principle factory
 * class to retrieve the values from the database and then store it as a claim value.
 */
