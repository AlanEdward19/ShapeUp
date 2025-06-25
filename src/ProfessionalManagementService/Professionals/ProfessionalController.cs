using Asp.Versioning;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using ProfessionalManagementService.Common.Interfaces;
using ProfessionalManagementService.Professionals.CreateProfessional;
using ProfessionalManagementService.Professionals.DeleteProfessional;
using ProfessionalManagementService.Professionals.GetProfessionalById;
using ProfessionalManagementService.Professionals.UpdateProfessional;
using SharedKernel.Filters;
using SharedKernel.Utils;

namespace ProfessionalManagementService.Professionals;

[ApiVersion("1.0")]
[ApiController]
[TokenValidatorFilter]
[Route("v{version:apiVersion}/[Controller]")]
public class ProfessionalController : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProfessionalById(string id,
        [FromServices] IHandler<ProfessionalDto, GetProfessionalByIdQuery> handler,
        CancellationToken cancellationToken)
    {
        var query = new GetProfessionalByIdQuery(id);
        var professional = await handler.HandleAsync(query, cancellationToken);

        return Ok(professional);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateProfessional([FromBody] CreateProfessionalCommand command,
        [FromServices] IHandler<ProfessionalDto, CreateProfessionalCommand> handler,
        CancellationToken cancellationToken)
    {
        string userId = User.GetObjectId();
        command.SetId(userId);
        
        var professional = await handler.HandleAsync(command, cancellationToken);
        return Created(HttpContext.Request.GetDisplayUrl(), professional);
    }
    
    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateProfessional(string id,
        [FromBody] UpdateProfessionalCommand command,
        [FromServices] IHandler<ProfessionalDto, UpdateProfessionalCommand> handler,
        CancellationToken cancellationToken)
    {
        command.SetId(id);
        var professional = await handler.HandleAsync(command, cancellationToken);
        
        return Ok(professional);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProfessional(string id,
        [FromServices] IHandler<bool, DeleteProfessionalCommand> handler,
        CancellationToken cancellationToken)
    {
        var command = new DeleteProfessionalCommand(id);
        await handler.HandleAsync(command, cancellationToken);

        return NoContent();
    }
}