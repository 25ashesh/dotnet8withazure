using FrontLineCleaners.Application.Commands;
using FrontLineCleaners.Application.Dtos;
using FrontLineCleaners.Application.Queries;
using FrontLineCleaners.Domain.Constants;
using FrontLineCleaners.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FrontLineCleaners.API.Controllers;

[ApiController]
[Route("api/cleaners")]
[Authorize]
public class CleanersController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CleanerDto>))]
    //[Authorize(Policy = Constants.PolicyNames.OwnedAtLeast2CleanerCompanies)]
    public async Task<ActionResult<IEnumerable<CleanerDto>>> GetAll([FromQuery] GetAllCleanersQuery query)
    {
        var cleaners = await mediator.Send(query);
        return Ok(cleaners);
    }
    [HttpGet("{id}")]
    [Authorize(Policy = Infrastructure.Constants.PolicyNames.HasNationality)]
    //[Route("{id}")]
    public async Task<ActionResult<CleanerDto>> GetById([FromRoute] int id)
    {
        var cleaner = await mediator.Send(new GetCleanerByIdQuery(id));

        return Ok(cleaner);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCleaner([FromRoute] int id)
    {
        await mediator.Send(new DeleteCleanerCommand(id));

        return NoContent();
    }

    [HttpPost]
    [Authorize(Roles = UserRoles.Owner)]
    public async Task<IActionResult> CreateCleaner([FromBody] CreateCleanerCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        int id = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCleaner([FromRoute] int id, [FromBody] UpdateCleanerCommand command)
    {
        command.Id = id;
        await mediator.Send(command);

        return NoContent();
    }
}
