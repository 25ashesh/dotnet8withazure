using FrontLineCleaners.API.Middlewares;
using FrontLineCleaners.API.Services;
using FrontLineCleaners.Application.Extensions;
using FrontLineCleaners.Infrastructure.Extensions;
using FrontLineCleaners.Infrastructure.Seeders;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<RequestTimingMiddleware>();
builder.Services.AddScoped<IWeatherForecastService, WeatherForecastService>();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration)
);

var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IFrontLineCleanersSeeder>();
await seeder.Seed();

// Configure the HTTP request pipeline.

app.UseMiddleware<RequestTimingMiddleware>();
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
