using Asp.Versioning;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Filters;
using SharedKernel.Utils;
using TrainingService.Common.Interfaces;
using TrainingService.WorkoutSessions.Common.Dto;
using TrainingService.WorkoutSessions.CreateWorkoutSession;
using TrainingService.WorkoutSessions.DeleteWorkoutSessionById;
using TrainingService.WorkoutSessions.GetCurrentWorkoutSessionByUserId;
using TrainingService.WorkoutSessions.GetWorkoutSessionById;
using TrainingService.WorkoutSessions.GetWorkoutSessionsByUserId;
using TrainingService.WorkoutSessions.GetWorkoutSessionsByWorkoutId;
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
    /// <summary>
    /// Rota para criar uma nova sessão de treino.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateWorkoutSession([FromBody] CreateWorkoutSessionCommand command,
        [FromServices] IHandler<WorkoutSessionDto, CreateWorkoutSessionCommand> handler,
        CancellationToken cancellationToken)
    {
        string userId = User.GetObjectId();
        command.SetUserId(userId);

        return Created(Request.GetDisplayUrl(), await handler.HandleAsync(command, cancellationToken));
    }

    /// <summary>
    /// Rota para obter uma sessão de treino específica pelo ID da sessão.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetWorkoutSessionById(Guid id,
        [FromServices] IHandler<WorkoutSessionDto, GetWorkoutSessionByIdQuery> handler,
        CancellationToken cancellationToken)
    {
        GetWorkoutSessionByIdQuery query = new(id.ToString());
        return Ok(await handler.HandleAsync(query, cancellationToken));
    }
    
    /// <summary>
    /// Rota para obter a sessão de treino atual associada ao usuário autenticado.
    /// </summary>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("CurrentWorkoutSession")]
    public async Task<IActionResult> GetCurrentWorkoutSessionByUserId(
        [FromServices] IHandler<WorkoutSessionDto, GetCurrentWorkoutSessionByUserIdQuery> handler,
        CancellationToken cancellationToken)
    {
        string userId = User.GetObjectId();
        GetCurrentWorkoutSessionByUserIdQuery query = new(userId);
        return Ok(await handler.HandleAsync(query, cancellationToken));
    }

    /// <summary>
    /// Rota para obter as sessões de treino associadas a um usuário específico pelo ID do usuário.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("/v{version:apiVersion}/User/{userId}/WorkoutSession")]
    public async Task<IActionResult> GetWorkoutSessionsByUserId(string userId,
        [FromServices] IHandler<ICollection<WorkoutSessionDto>, GetWorkoutSessionsByUserIdQuery> handler,
        CancellationToken cancellationToken)
    {
        GetWorkoutSessionsByUserIdQuery query = new(userId);
        return Ok(await handler.HandleAsync(query, cancellationToken));
    }
    
    /// <summary>
    /// Rota para obter as sessões de treino associadas a um treino específico pelo ID do treino.
    /// </summary>
    /// <param name="workoutId"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("/v{version:apiVersion}/Workout/{workoutId:guid}/WorkoutSession")]
    public async Task<IActionResult> GetWorkoutSessionsByUserId(Guid workoutId,
        [FromServices] IHandler<ICollection<WorkoutSessionDto>, GetWorkoutSessionsByWorkoutIdQuery> handler,
        CancellationToken cancellationToken)
    {
        GetWorkoutSessionsByWorkoutIdQuery query = new(workoutId);
        return Ok(await handler.HandleAsync(query, cancellationToken));
    }

    /// <summary>
    /// Route para atualizar uma sessão de treino pelo ID da sessão.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="command"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateWorkoutSessionById(Guid id, [FromBody] UpdateWorkoutSessionByIdCommand command,
        [FromServices] IHandler<WorkoutSessionDto, UpdateWorkoutSessionByIdCommand> handler,
        CancellationToken cancellationToken)
    {
        command.SetSessionId(id.ToString());
        command.SetUserId(User.GetObjectId());
        return Ok(await handler.HandleAsync(command, cancellationToken));
    }

    /// <summary>
    /// Route para deletar uma sessão de treino pelo ID da sessão.
    /// </summary>
    /// <param name="handler"></param>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteWorkoutSessionById(
        [FromServices] IHandler<bool, DeleteWorkoutSessionByIdCommand> handler,
        Guid id, CancellationToken cancellationToken)
    {
        DeleteWorkoutSessionByIdCommand command = new();
        command.SetSessionId(id);
        command.SetUserId(User.GetObjectId());

        await handler.HandleAsync(command, cancellationToken);
        return NoContent();
    }
}