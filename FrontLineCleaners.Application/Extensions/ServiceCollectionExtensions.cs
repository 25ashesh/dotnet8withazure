using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace FrontLineCleaners.Application.Extensions;

public static class ServiceCollectionsExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        var applicationAssembly = typeof(ServiceCollectionsExtensions).Assembly;
        //services.AddScoped<ICleanersService, CleanersService>();

        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(applicationAssembly));

        services.AddAutoMapper(applicationAssembly);

        services.AddValidatorsFromAssembly(applicationAssembly)
            .AddFluentValidationAutoValidation();
    }
}

