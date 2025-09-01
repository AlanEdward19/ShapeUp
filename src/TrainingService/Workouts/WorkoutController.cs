using Asp.Versioning;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Enums;
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

/// <summary>
/// Controller para gerenciar treinos.
/// </summary>
[ApiVersion("1.0")]
[ApiController]
[TokenValidatorFilter]
[Route("v{version:apiVersion}/[Controller]")]
public class WorkoutController : ControllerBase
{
    /// <summary>
    /// Rota para obter um treino por id.
    /// </summary>
    /// <param name="handler"></param>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WorkoutDto))]
    public async Task<IActionResult> GetWorkoutById([FromServices] IHandler<WorkoutDto, GetWorkoutByIdQuery> handler,
        Guid id, CancellationToken cancellationToken)
    {
        GetWorkoutByIdQuery query = new();
        query.SetWorkoutId(id);
        
        return Ok(await handler.HandleAsync(query, cancellationToken));
    }
    
    /// <summary>
    /// Rota para obter treinos por id do usuário autenticado.
    /// </summary>
    /// <param name="handler"></param>
    /// <param name="userId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("/v{version:apiVersion}/User/{userId}/Workout")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<WorkoutDto>))]
    public async Task<IActionResult> GetWorkoutsByUserId([FromServices] IHandler<ICollection<WorkoutDto>, GetWorkoutsByUserIdQuery> handler,
        string userId, CancellationToken cancellationToken)
    {
        GetWorkoutsByUserIdQuery query = new();
        query.SetUserId(userId);
        
        return Ok(await handler.HandleAsync(query, cancellationToken));
    }
    
    /// <summary>
    /// Método para criar um treino para o usuário autenticado.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(WorkoutDto))]
    public async Task<IActionResult> CreateForOwnUserWorkout([FromBody] CreateWorkoutCommand command,
        [FromServices] IHandler<WorkoutDto, CreateWorkoutCommand> handler, CancellationToken cancellationToken)
    {
        string userId = User.GetObjectId();
        command.SetCreatorId(userId);
        command.SetUserId(userId);
        
        return Created(Request.GetDisplayUrl(), await handler.HandleAsync(command, cancellationToken));
    }
    
    /// <summary>
    /// Método para criar um treino para um usuário específico.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="userId"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("/v{version:apiVersion}/User/{userId}/Workout")]
    [AuthFilter(EPermissionAction.Write, "workout")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(WorkoutDto))]
    public async Task<IActionResult> CreateWorkout([FromBody] CreateWorkoutCommand command, string userId,
        [FromServices] IHandler<WorkoutDto, CreateWorkoutCommand> handler, CancellationToken cancellationToken)
    {
        string loggedUser = User.GetObjectId();
        command.SetCreatorId(loggedUser);
        command.SetUserId(userId);
        
        return Created(Request.GetDisplayUrl(), await handler.HandleAsync(command, cancellationToken));
    }
    
    /// <summary>
    /// Método para atualizar um treino por id.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="command"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WorkoutDto))]
    public async Task<IActionResult> UpdateWorkout(Guid id, [FromBody] UpdateWorkoutByIdCommand command,
        [FromServices] IHandler<WorkoutDto, UpdateWorkoutByIdCommand> handler, CancellationToken cancellationToken)
    {
        string loggedUser = User.GetObjectId();
        command.SetUserId(loggedUser);
        command.SetWorkoutId(id);
        
        return Ok(await handler.HandleAsync(command, cancellationToken));
    }
    
    /// <summary>
    /// Método para deletar um treino por id.
    /// </summary>
    /// <param name="handler"></param>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteWorkout([FromServices] IHandler<bool, DeleteWorkoutByIdCommand> handler,
        Guid id, CancellationToken cancellationToken)
    {
        DeleteWorkoutByIdCommand command = new();
        command.SetWorkoutId(id);
        command.SetUserId(User.GetObjectId());
        
        await handler.HandleAsync(command, cancellationToken);
        return NoContent();
    }
}