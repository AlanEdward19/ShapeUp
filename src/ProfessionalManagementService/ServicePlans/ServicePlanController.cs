using Asp.Versioning;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using ProfessionalManagementService.Clients;
using ProfessionalManagementService.Common.Enums;
using ProfessionalManagementService.Common.Interfaces;
using ProfessionalManagementService.ServicePlans.AddServicePlanToClient;
using ProfessionalManagementService.ServicePlans.ChangeServicePlanStatusFromClient;
using ProfessionalManagementService.ServicePlans.CreateServicePlan;
using ProfessionalManagementService.ServicePlans.DeleteServicePlan;
using ProfessionalManagementService.ServicePlans.DeleteServicePlanFromClient;
using ProfessionalManagementService.ServicePlans.GetServicePlanById;
using ProfessionalManagementService.ServicePlans.GetServicePlansByProfessionalId;
using ProfessionalManagementService.ServicePlans.UpdateServicePlan;
using SharedKernel.Filters;
using SharedKernel.Utils;

namespace ProfessionalManagementService.ServicePlans;

[ApiVersion("1.0")]
[ApiController]
[TokenValidatorFilter]
[Route("v{version:apiVersion}/[Controller]")]
public class ServicePlanController : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetServicePlanById(Guid id,
        [FromServices] IHandler<ServicePlanDto, GetServicePlanByIdQuery> handler,
        CancellationToken cancellationToken)
    {
        var query = new GetServicePlanByIdQuery(id);
        var servicePlan = await handler.HandleAsync(query, cancellationToken);

        return Ok(servicePlan);
    }

    [HttpGet("/v{version:apiVersion}/Professional/{professionalId}/ServicePlan")]
    public async Task<IActionResult> GetServicePlanByProfessionalId(string professionalId,
        [FromServices] IHandler<List<ServicePlanDto>, GetServicePlansByProfessionalIdQuery> handler,
        CancellationToken cancellationToken)
    {
        var query = new GetServicePlansByProfessionalIdQuery(professionalId);
        var servicePlans = await handler.HandleAsync(query, cancellationToken);

        return Ok(servicePlans);
    }

    [HttpPost]
    public async Task<IActionResult> CreateServicePlan([FromBody] CreateServicePlanCommand command,
        [FromServices] IHandler<ServicePlanDto, CreateServicePlanCommand> handler,
        CancellationToken cancellationToken)
    {
        string userId = User.GetObjectId();
        command.SetProfessionalId(userId);

        var servicePlan = await handler.HandleAsync(command, cancellationToken);
        return Created(HttpContext.Request.GetDisplayUrl(), servicePlan);
    }

    [HttpPost("{id:guid}/Client/{clientId}")]
    public async Task<IActionResult> AddServicePlanToClient(Guid id, string clientId,
        [FromServices] IHandler<ClientDto, AddServicePlanToClientCommand> handler,
        CancellationToken cancellationToken)
    {
        string userId = User.GetObjectId();
        var command = new AddServicePlanToClientCommand(clientId, id, userId);

        var client = await handler.HandleAsync(command, cancellationToken);

        return Created(Request.GetDisplayUrl(), client);
    }

    [HttpDelete("{id:guid}/Client/{clientId}")]
    public async Task<IActionResult> DeleteServicePlanFromClient(Guid id, string clientId,
        [FromServices] IHandler<ClientDto, DeleteServicePlanFromClientCommand> handler,
        CancellationToken cancellationToken)
    {
        string userId = User.GetObjectId();
        var command = new DeleteServicePlanFromClientCommand(clientId, id, userId);

        var client = await handler.HandleAsync(command, cancellationToken);

        return Ok(client);
    }

    [HttpPut("{id:guid}/Client/{clientId}")]
    public async Task<IActionResult> ChangeServicePlanStatusFromClient(Guid id, string clientId,
        [FromBody] ChangeServicePlanStatusFromClientValueObject changeServicePlanStatusFromClientValueObject,
        [FromServices] IHandler<ClientDto, ChangeServicePlanStatusFromClientCommand> handler,
        CancellationToken cancellationToken)
    {
        string userId = User.GetObjectId();
        var command = new ChangeServicePlanStatusFromClientCommand(clientId, id, userId, changeServicePlanStatusFromClientValueObject.Status, changeServicePlanStatusFromClientValueObject.Reason);

        var client = await handler.HandleAsync(command, cancellationToken);

        return Ok(client);
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> UpdateServicePlan(Guid id,
        [FromBody] UpdateServicePlanCommand command,
        [FromServices] IHandler<ServicePlanDto, UpdateServicePlanCommand> handler,
        CancellationToken cancellationToken)
    {
        command.SetId(id);
        var servicePlan = await handler.HandleAsync(command, cancellationToken);

        return Ok(servicePlan);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteServicePlan(Guid id,
        [FromServices] IHandler<bool, DeleteServicePlanCommand> handler,
        CancellationToken cancellationToken)
    {
        var command = new DeleteServicePlanCommand(id);

        await handler.HandleAsync(command, cancellationToken);

        return NoContent();
    }
}