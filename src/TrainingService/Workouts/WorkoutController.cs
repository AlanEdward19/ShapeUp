using Asp.Versioning;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Filters;
using SharedKernel.Utils;
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
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WorkoutDto))]
    public async Task<IActionResult> GetWorkoutById([FromServices] IHandler<WorkoutDto, GetWorkoutByIdQuery> handler,
        Guid id, CancellationToken cancellationToken)
    {
        GetWorkoutByIdQuery query = new();
        query.SetWorkoutId(id);
        
        return Ok(await handler.HandleAsync(query, cancellationToken));
    }
    
    [HttpGet("/v{version:apiVersion}/User/{userId}/Workout")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<WorkoutDto>))]
    public async Task<IActionResult> GetWorkoutsByUserId([FromServices] IHandler<ICollection<WorkoutDto>, GetWorkoutsByUserIdQuery> handler,
        string userId, CancellationToken cancellationToken)
    {
        GetWorkoutsByUserIdQuery query = new();
        query.SetUserId(userId);
        
        return Ok(await handler.HandleAsync(query, cancellationToken));
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateForOwnUserWorkout([FromBody] CreateWorkoutCommand command,
        [FromServices] IHandler<WorkoutDto, CreateWorkoutCommand> handler, CancellationToken cancellationToken)
    {
        string userId = User.GetObjectId();
        command.SetCreatorId(userId);
        command.SetUserId(userId);
        
        return Created(Request.GetDisplayUrl(), await handler.HandleAsync(command, cancellationToken));
    }
    
    [HttpPost("/v{version:apiVersion}/User/{userId}/Workout")]
    public async Task<IActionResult> CreateWorkout([FromBody] CreateWorkoutCommand command, string userId,
        [FromServices] IHandler<WorkoutDto, CreateWorkoutCommand> handler, CancellationToken cancellationToken)
    {
        string loggedUser = User.GetObjectId();
        command.SetCreatorId(loggedUser);
        command.SetUserId(userId);
        
        return Created(Request.GetDisplayUrl(), await handler.HandleAsync(command, cancellationToken));
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateWorkout(Guid id, [FromBody] UpdateWorkoutByIdCommand command,
        [FromServices] IHandler<WorkoutDto, UpdateWorkoutByIdCommand> handler, CancellationToken cancellationToken)
    {
        string loggedUser = User.GetObjectId();
        command.SetUserId(loggedUser);
        command.SetWorkoutId(id);
        
        return Ok(await handler.HandleAsync(command, cancellationToken));
    }
    
    [HttpDelete("{id:guid}")]
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