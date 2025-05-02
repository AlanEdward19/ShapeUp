using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Filters;
using TrainingService.Common.Interfaces;
using TrainingService.Workouts.Common;
using TrainingService.Workouts.CreateWorkout;
using TrainingService.Workouts.DeleteWorkoutById;
using TrainingService.Workouts.GetWorkoutById;
using TrainingService.Workouts.GetWorkoutsByUserId;
using TrainingService.Workouts.UpdateWorkoutById;

namespace TrainingService.Workouts;

[ApiVersion("1.0")]
[ApiController]
[TokenValidatorFilter]
[Route("v{version:apiVersion}/[Controller]")]
public class WorkoutController : ControllerBase
{
    [HttpGet("{id:guid}/getWorkout")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WorkoutDto))]
    public async Task<IActionResult> GetWorkoutById([FromServices] IHandler<WorkoutDto, GetWorkoutByIdQuery> handler,
        Guid id, CancellationToken cancellationToken)
    {
        GetWorkoutByIdQuery query = new();
        query.SetWorkoutId(id);
        
        return Ok(await handler.HandleAsync(query, cancellationToken));
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<WorkoutDto>))]
    public async Task<IActionResult> GetWorkoutsByUserId([FromServices] IHandler<ICollection<WorkoutDto>, GetWorkoutsByUserIdQuery> handler,
        string userId, CancellationToken cancellationToken)
    {
        GetWorkoutsByUserIdQuery query = new();
        query.SetUserId(userId);
        
        return Ok(await handler.HandleAsync(query, cancellationToken));
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateWorkout([FromBody] CreateWorkoutCommand command,
        [FromServices] IHandler<bool, CreateWorkoutCommand> handler, CancellationToken cancellationToken)
    {
        await handler.HandleAsync(command, cancellationToken);
        return Created();
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateWorkout([FromBody] UpdateWorkoutByIdCommand command,
        [FromServices] IHandler<bool, UpdateWorkoutByIdCommand> handler, CancellationToken cancellationToken)
    {
        await handler.HandleAsync(command, cancellationToken);
        return Created();
    }
    
    [HttpDelete("{id:guid}/deleteWorkout")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteWorkout([FromServices] IHandler<bool, DeleteWorkoutByIdCommand> handler,
        Guid id, CancellationToken cancellationToken)
    {
        DeleteWorkoutByIdCommand command = new();
        command.SetWorkoutId(id);
        
        await handler.HandleAsync(command, cancellationToken);
        return NoContent();
    }
}