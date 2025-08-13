using Asp.Versioning;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Filters;
using TrainingService.Common.Interfaces;
using TrainingService.Exercises.Common;
using TrainingService.Exercises.Common.Enums;
using TrainingService.Exercises.CreateExercise;
using TrainingService.Exercises.DeleteExerciseById;
using TrainingService.Exercises.GetExerciseById;
using TrainingService.Exercises.GetExerciseByMuscleGroup;
using TrainingService.Exercises.UpdateExercise;

namespace TrainingService.Exercises;

/// <summary>
/// Controller responsável por gerenciar os exercícios
/// </summary>
[ApiVersion("1.0")]
[ApiController]
[TokenValidatorFilter]
[Route("v{version:apiVersion}/[Controller]")]
public class ExerciseController : ControllerBase
{
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExerciseDto))]
    public async Task<IActionResult> GetExerciseById([FromServices] IHandler<ExerciseDto, GetExerciseByIdQuery> handler,
        Guid id, CancellationToken cancellationToken)
    {
        GetExerciseByIdQuery query = new();
        query.SetExerciseId(id);
        
        return Ok(await handler.HandleAsync(query, cancellationToken));
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ExerciseDto>))]
    public async Task<IActionResult> GetExercises([FromServices] IHandler<ICollection<ExerciseDto>, GetExerciseByMuscleGroupQuery> handler,
        EMuscleGroup? muscleGroup, CancellationToken cancellationToken)
    {
        GetExerciseByMuscleGroupQuery query = new();
        query.SetMuscleGroup(muscleGroup);
        
        return Ok(await handler.HandleAsync(query, cancellationToken));
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateExercise([FromBody] CreateExerciseCommand command,
        [FromServices] IHandler<ExerciseDto, CreateExerciseCommand> handler, CancellationToken cancellationToken)
    {
        return Created(Request.GetDisplayUrl(), await handler.HandleAsync(command, cancellationToken));
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateExercise([FromBody] UpdateExerciseCommand command,
        [FromServices] IHandler<ExerciseDto, UpdateExerciseCommand> handler, CancellationToken cancellationToken)
    {
        return Ok(await handler.HandleAsync(command, cancellationToken));
    }
    
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteExercise([FromServices] IHandler<bool, DeleteExerciseByIdCommand> handler,
        Guid id, CancellationToken cancellationToken)
    {
        DeleteExerciseByIdCommand command = new();
        command.SetExerciseId(id);
        
        await handler.HandleAsync(command, cancellationToken);
        return NoContent();
    }
}