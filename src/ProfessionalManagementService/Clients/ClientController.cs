using Asp.Versioning;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using ProfessionalManagementService.Clients.CreateClient;
using ProfessionalManagementService.Clients.DeleteClient;
using ProfessionalManagementService.Clients.GetClientById;
using ProfessionalManagementService.Clients.GetClientsByProfessionalId;
using ProfessionalManagementService.Clients.UpdateClient;
using ProfessionalManagementService.Common.Interfaces;
using SharedKernel.Filters;
using SharedKernel.Utils;

namespace ProfessionalManagementService.Clients;

[ApiVersion("1.0")]
[ApiController]
[TokenValidatorFilter]
[Route("v{version:apiVersion}/[Controller]")]
public class ClientController : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetClientById(string id,
        [FromServices] IHandler<ClientDto, GetClientByIdQuery> handler,
        CancellationToken cancellationToken)
    {
        var query = new GetClientByIdQuery(id);

        return Ok(await handler.HandleAsync(query, cancellationToken));
    }

    [HttpGet("/Professional/{professionalId}/Client")]
    public async Task<IActionResult> GetClientsByProfessionalId(string professionalId,
        [FromServices] IHandler<List<ClientDto>, GetClientsByProfessionalIdQuery> handler,
        CancellationToken cancellationToken)
    {
        var query = new GetClientsByProfessionalIdQuery(professionalId);

        return Ok(await handler.HandleAsync(query, cancellationToken));
    }

    [HttpPost]
    public async Task<IActionResult> CreateClient([FromServices] IHandler<ClientDto, CreateClientCommand> handler,
        CancellationToken cancellationToken)
    {
        string userId = User.GetObjectId();
        string email = User.GetEmail();

        var command = new CreateClientCommand(userId, email);

        return Created(HttpContext.Request.GetDisplayUrl(), await handler.HandleAsync(command, cancellationToken));
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateClient(string id,
        [FromBody] UpdateClientCommand command,
        [FromServices] IHandler<ClientDto, UpdateClientCommand> handler,
        CancellationToken cancellationToken)
    {
        string userId = User.GetObjectId();
        command.SetId(userId);
        
        var client = await handler.HandleAsync(command, cancellationToken);

        return Ok(client);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClient(string id,
        [FromServices] IHandler<bool, DeleteClientCommand> handler,
        CancellationToken cancellationToken)
    {
        var command = new DeleteClientCommand(id);
        await handler.HandleAsync(command, cancellationToken);

        return NoContent();
    }
}