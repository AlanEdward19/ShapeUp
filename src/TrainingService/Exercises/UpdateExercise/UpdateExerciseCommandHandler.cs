using SharedKernel.Exceptions;
using TrainingService.Common.Interfaces;
using TrainingService.Exercises.Common;
using TrainingService.Exercises.Common.Repository;

namespace TrainingService.Exercises.UpdateExercise;

/// <summary>
///     Handler para atualizar um exercício
/// </summary>
/// <param name="repository"></param>
public class UpdateExerciseCommandHandler(IExerciseRepository repository)
    : IHandler<ExerciseDto, UpdateExerciseCommand>
{
    /// <summary>
    /// Método para atualizar um exercício.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<ExerciseDto> HandleAsync(UpdateExerciseCommand command, CancellationToken cancellationToken)
    {
        Exercise? exercise = await repository.GetExerciseByIdAsync(command.Id, cancellationToken);
        
        if (exercise is null)
            throw new NotFoundException($"Exercise with ID '{command.Id}' not found.");
        
        exercise.UpdateExercise(command);
        
        await repository.UpdateAsync(exercise, cancellationToken);
        
        return new ExerciseDto(exercise);
    }
}