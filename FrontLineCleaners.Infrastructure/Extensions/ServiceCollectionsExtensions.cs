using FrontLineCleaners.Domain.Entities;
using FrontLineCleaners.Domain.Repositories;
using FrontLineCleaners.Infrastructure.Authorization;
using FrontLineCleaners.Infrastructure.Authorization.Requirements;
using FrontLineCleaners.Infrastructure.Persistence;
using FrontLineCleaners.Infrastructure.Repositories;
using FrontLineCleaners.Infrastructure.Seeders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FrontLineCleaners.Infrastructure.Extensions;

public static class ServiceCollectionsExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration) 
    {
        var connectionString = configuration.GetConnectionString("FrontLineCleanersDb");
        services.AddDbContext<FrontLineCleanersDbContext>(options => 
        options.UseSqlServer(connectionString)
        .EnableSensitiveDataLogging());

        services.AddIdentityApiEndpoints<User>() //exposes endpoints for login/register etc.
            .AddRoles<IdentityRole>() //Adds roles
            .AddClaimsPrincipalFactory<FrontLineUserClaimsPrincipalFactory>() //Use the customized claims factory instead of the default one
            .AddEntityFrameworkStores<FrontLineCleanersDbContext>();

        services.AddScoped<IFrontLineCleanersSeeder, FrontLineCleanersSeeder>();
        services.AddScoped<ICleanersRepository, CleanersRepository>();
        services.AddScoped<IServicesRepository, ServicesRepository>();

        //Defining value e.g. as Nepalese implies, the nationality should match
        services.AddAuthorizationBuilder()
            .AddPolicy(Constants.PolicyNames.HasNationality, builder => builder.RequireClaim(Constants.AppClaimTypes.Nationality, "Nepalese", "Indian", "Chinese"))
            .AddPolicy(Constants.PolicyNames.AtLeast20, builder => builder.AddRequirements(new MinimumAgeRequirement(20)));

        services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();
    }
}
