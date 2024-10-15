using FrontLineCleaners.Application.Commands;
using FrontLineCleaners.Application.Dtos;
using FrontLineCleaners.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FrontLineCleaners.API.Controllers;

[ApiController]
[Route("api/cleaners/{cleanerId}/services")]
[Authorize]
public class ServicesController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateService([FromRoute]int cleanerId,[FromBody] CreateServiceCommand command) 
    {
        command.CleanerId = cleanerId;
        var serviceId = await mediator.Send(command);
        return CreatedAtAction(nameof(GetByIdForCleaner), new { cleanerId, serviceId }, null);
    }

    [HttpGet]
    [Authorize(Policy = Infrastructure.Constants.PolicyNames.AtLeast20)]
    public async Task<ActionResult<IEnumerable<ServiceDto>>> GetAllServicesForCleaner([FromRoute] int cleanerId) 
    {
        var services = await mediator.Send(new GetServicesForCleanerQuery(cleanerId));
        return Ok(services);
    }

    [HttpGet("{serviceId}")]
    public async Task<ActionResult<ServiceDto>> GetByIdForCleaner([FromRoute] int cleanerId, [FromRoute] int serviceId)
    {
        var service = await mediator.Send(new GetServiceByIdForCleanerQuery(cleanerId, serviceId));
        return Ok(service);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteServices([FromRoute] int cleanerId)
    {
        await mediator.Send(new DeleteServicesForCleanerCommand(cleanerId));
        return NoContent();
    }
}
