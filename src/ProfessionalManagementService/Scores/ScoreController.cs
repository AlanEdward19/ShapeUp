using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using ProfessionalManagementService.Common.Interfaces;
using ProfessionalManagementService.Professionals.GetProfessionals;
using ProfessionalManagementService.Scores.GetProfessionalScoreByProfessionalId;
using SharedKernel.Filters;

namespace ProfessionalManagementService.Scores;

[ApiVersion("1.0")]
[ApiController]
[TokenValidatorFilter]
[Route("v{version:apiVersion}/[Controller]")]
public class ScoreController : ControllerBase
{
    [HttpGet("/v{version:apiVersion}/Professional/{professionalId}/Score")]
    public async Task<IActionResult> GetProfessionals(
        string professionalId,
        [FromServices] IHandler<ProfessionalScore, GetProfessionalScoreByProfessionalIdQuery> handler,
        CancellationToken cancellationToken)
    {
        var query = new GetProfessionalScoreByProfessionalIdQuery(professionalId);
        var score = await handler.HandleAsync(query, cancellationToken);

        return Ok(score);
    }
}