using Asp.Versioning;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using ProfessionalManagementService.Common.Interfaces;
using ProfessionalManagementService.Professionals.CreateProfessional;
using ProfessionalManagementService.Professionals.DeleteProfessional;
using ProfessionalManagementService.Professionals.GetProfessionalById;
using ProfessionalManagementService.Professionals.GetProfessionals;
using ProfessionalManagementService.Professionals.UpdateProfessional;
using SharedKernel.Enums;
using SharedKernel.Filters;
using SharedKernel.Utils;

namespace ProfessionalManagementService.Professionals;

[ApiVersion("1.0")]
[ApiController]
[TokenValidatorFilter]
[Route("v{version:apiVersion}/[Controller]")]
public class ProfessionalController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetProfessionals([FromServices] IHandler<List<ProfessionalDto>, GetProfessionalsQuery> handler,
        CancellationToken cancellationToken)
    {
        string userId = User.GetObjectId();
        
        var query = new GetProfessionalsQuery(userId);
        var professionals = await handler.HandleAsync(query, cancellationToken);

        return Ok(professionals);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProfessionalById(string id,
        [FromServices] IHandler<ProfessionalDto, GetProfessionalByIdQuery> handler,
        CancellationToken cancellationToken)
    {
        var query = new GetProfessionalByIdQuery(id);
        var professional = await handler.HandleAsync(query, cancellationToken);

        return Ok(professional);
    }
    
    [HttpPost("{id}")]
    [AuthFilter(EPermissionAction.Write, "professional")]
    public async Task<IActionResult> CreateProfessional(string id, [FromBody] CreateProfessionalCommand command,
        [FromServices] IHandler<ProfessionalDto, CreateProfessionalCommand> handler,
        CancellationToken cancellationToken)
    {
        command.SetId(id);
        
        var professional = await handler.HandleAsync(command, cancellationToken);
        return Created(HttpContext.Request.GetDisplayUrl(), professional);
    }
    
    [HttpPatch("{id}")]
    [AuthFilter(EPermissionAction.Update, "professional")]
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
    [AuthFilter(EPermissionAction.Delete, "professional")]
    public async Task<IActionResult> DeleteProfessional(string id,
        [FromServices] IHandler<bool, DeleteProfessionalCommand> handler,
        CancellationToken cancellationToken)
    {
        var command = new DeleteProfessionalCommand(id);
        await handler.HandleAsync(command, cancellationToken);

        return NoContent();
    }
}