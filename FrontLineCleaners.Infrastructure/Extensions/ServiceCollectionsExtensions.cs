using FrontLineCleaners.Domain.Repositories;
using FrontLineCleaners.Infrastructure.Persistence;
using FrontLineCleaners.Infrastructure.Repositories;
using FrontLineCleaners.Infrastructure.Seeders;
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
    
        services.AddScoped<IFrontLineCleanersSeeder, FrontLineCleanersSeeder>();
        services.AddScoped<ICleanersRepository, CleanersRepository>();
        services.AddScoped<IServicesRepository, ServicesRepository>();
    }
}
