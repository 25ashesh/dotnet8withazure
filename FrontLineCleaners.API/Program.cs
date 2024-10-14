using FrontLineCleaners.API.Middlewares;
using FrontLineCleaners.Application.Extensions;
using FrontLineCleaners.Domain.Entities;
using FrontLineCleaners.Infrastructure.Extensions;
using FrontLineCleaners.Infrastructure.Seeders;
using Serilog;
using FrontLineCleaners.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddPresentation();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);


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

app.MapGroup("api/identity")
    .WithTags("Identity")
    .MapIdentityApi<User>();

app.UseAuthorization();

app.MapControllers();

app.Run();
