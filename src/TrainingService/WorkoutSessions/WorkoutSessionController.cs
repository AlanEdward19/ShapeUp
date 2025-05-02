using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Filters;
using TrainingService.Common.Interfaces;
using TrainingService.WorkoutSessions.CreateWorkoutSession;
using TrainingService.WorkoutSessions.DeleteWorkoutSessionById;
using TrainingService.WorkoutSessions.UpdateWorkoutSessionById;

namespace TrainingService.WorkoutSessions;

/// <summary>
/// Controller responsável por gerenciar as sessões de treino
/// </summary>
[ApiVersion("1.0")]
[ApiController]
[TokenValidatorFilter]
[Route("v{version:apiVersion}/[Controller]")]
public class WorkoutSessionController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateWorkoutSession([FromBody] CreateWorkoutSessionCommand command,
        [FromServices] IHandler<bool, CreateWorkoutSessionCommand> handler, CancellationToken cancellationToken)
    {
        await handler.HandleAsync(command, cancellationToken);
        return Created();
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateWorkoutSessionById([FromBody] UpdateWorkoutSessionByIdCommand command,
        [FromServices] IHandler<bool, UpdateWorkoutSessionByIdCommand> handler, CancellationToken cancellationToken)
    {
        await handler.HandleAsync(command, cancellationToken);
        return Created();
    }
    
    [HttpDelete("{id:guid}/deleteWorkoutSession")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteWorkoutSessionById([FromServices] IHandler<bool, DeleteWorkoutSessionByIdCommand> handler,
        Guid id, CancellationToken cancellationToken)
    {
        DeleteWorkoutSessionByIdCommand command = new();
        command.SetSessionId(id);
        
        await handler.HandleAsync(command, cancellationToken);
        return NoContent();
    }
}