using Asp.Versioning;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using ProfessionalManagementService.Clients.CreateClient;
using ProfessionalManagementService.Clients.DeleteClient;
using ProfessionalManagementService.Clients.GetClientById;
using ProfessionalManagementService.Clients.GetClientsByProfessionalId;
using ProfessionalManagementService.Clients.UpdateClient;
using ProfessionalManagementService.Common.Interfaces;
using SharedKernel.Exceptions;
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
        [FromServices] IHandler<ClientDto?, GetClientByIdQuery> queryHandler,
        [FromServices] IHandler<ClientDto, CreateClientCommand> commandHandler,
        CancellationToken cancellationToken)
    {
        string userId = User.GetObjectId();
        
        var query = new GetClientByIdQuery(id);
        
        var client = await queryHandler.HandleAsync(query, cancellationToken);

        if (client != null) return Ok(client);
        
        if (id.Equals(userId))
        {
            string email = User.GetEmail();
            string name = User.GetFullName();

            var command = new CreateClientCommand(userId, email, name);
            client = await commandHandler.HandleAsync(command, cancellationToken);
        }
        else
            throw new NotFoundException($"Client with Id: '{query.Id}' not found.");

        return Ok(client);
    }

    [HttpGet("/Professional/{professionalId}/Client")]
    public async Task<IActionResult> GetClientsByProfessionalId(string professionalId,
        [FromServices] IHandler<List<ClientDto>, GetClientsByProfessionalIdQuery> handler,
        CancellationToken cancellationToken)
    {
        var query = new GetClientsByProfessionalIdQuery(professionalId);

        return Ok(await handler.HandleAsync(query, cancellationToken));
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