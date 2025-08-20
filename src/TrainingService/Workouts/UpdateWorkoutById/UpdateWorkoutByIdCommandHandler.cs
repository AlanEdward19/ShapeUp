using SharedKernel.Exceptions;
using TrainingService.Common.Interfaces;
using TrainingService.Exercises.Common.Repository;
using TrainingService.Workouts.Common;
using TrainingService.Workouts.Common.Repository;

namespace TrainingService.Workouts.UpdateWorkoutById;

/// <summary>
/// Handler para o comando de atualização de treino por id.
/// </summary>
/// <param name="repository"></param>
/// <param name="exerciseRepository"></param>
public class UpdateWorkoutByIdCommandHandler(IWorkoutRepository repository, IExerciseRepository exerciseRepository)
    : IHandler<WorkoutDto, UpdateWorkoutByIdCommand>
{
    /// <summary>
    /// Método para tratar o comando de atualização de treino por id.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<WorkoutDto> HandleAsync(UpdateWorkoutByIdCommand command, CancellationToken cancellationToken)
    {
        Workout? workout = await repository.GetWorkoutAsync(command.GetWorkoutId(), cancellationToken, true);
        
        if(workout == null)
            throw new NotFoundException($"Workout with ID {command.GetWorkoutId()} does not exist.");
        
        if (workout.CreatorId != command.GetUserId())
            throw new ForbiddenException(
                $"User {command.GetUserId()} is not authorized to update this workout.");
        
        workout.UpdateWorkout(command);
        
        if (command.Exercises.Any())
        {
            var exercises =
                (await exerciseRepository.GetExercisesByIdsAsync(command.Exercises.ToList(), cancellationToken, true))
                .ToList();
            
            workout.UpdateWorkoutExercises(exercises);
        }
        
        await repository.UpdateAsync(workout, cancellationToken);
        
        return new WorkoutDto(workout);
    }
}