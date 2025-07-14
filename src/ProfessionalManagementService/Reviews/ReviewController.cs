using Asp.Versioning;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using ProfessionalManagementService.Common.Interfaces;
using ProfessionalManagementService.Reviews.CreateReview;
using ProfessionalManagementService.Reviews.DeleteReview;
using ProfessionalManagementService.Reviews.GetReviewsByProfessionalId;
using ProfessionalManagementService.Reviews.UpdateReview;
using SharedKernel.Filters;
using SharedKernel.Utils;

namespace ProfessionalManagementService.Reviews;

[ApiVersion("1.0")]
[ApiController]
[TokenValidatorFilter]
[Route("v{version:apiVersion}/[Controller]")]
public class ReviewController : ControllerBase
{
    [HttpGet("/v{version:apiVersion}/Professional/{professionalId}/Review")]
    public async Task<IActionResult> GetReviewsByProfessionalIdAsync(
        [FromRoute] string professionalId,
        [FromServices] IHandler<List<ClientProfessionalReviewDto>, GetReviewsByProfessionalIdQuery> handler,
        CancellationToken cancellationToken)
    {
        var query = new GetReviewsByProfessionalIdQuery(professionalId);
        var reviews = await handler.HandleAsync(query, cancellationToken);

        return Ok(reviews);
    }

    [HttpPost("/v{version:apiVersion}/Professional/{professionalId}/ServicePlan/{servicePlanId:guid}/Review")]
    public async Task<IActionResult> CreateReviewAsync(
        [FromRoute] string professionalId,
        [FromRoute] Guid servicePlanId,
        [FromBody] CreateReviewCommand command,
        [FromServices] IHandler<ClientProfessionalReviewDto, CreateReviewCommand> handler,
        CancellationToken cancellationToken)
    {
        string clientId = User.GetObjectId();

        command.SetClientId(clientId);
        command.SetProfessionalId(professionalId);
        command.SetServicePlanId(servicePlanId);

        var review = await handler.HandleAsync(command, cancellationToken);

        return Created(HttpContext.Request.GetDisplayUrl(), review);
    }
    
    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> UpdateReviewAsync(
        [FromRoute] Guid id,
        [FromBody] UpdateReviewCommand command,
        [FromServices] IHandler<ClientProfessionalReviewDto, UpdateReviewCommand> handler,
        CancellationToken cancellationToken)
    {
        command.SetId(id);
        
        var updatedReview = await handler.HandleAsync(command, cancellationToken);

        return Ok(updatedReview);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteReviewAsync(
        [FromRoute] Guid id,
        [FromServices] IHandler<bool, DeleteReviewCommand> handler,
        CancellationToken cancellationToken)
    {
        var command = new DeleteReviewCommand(id);
        var result = await handler.HandleAsync(command, cancellationToken);

        if (result)
            return NoContent();

        return NotFound($"Review with ID '{id}' does not exist.");
    }
}