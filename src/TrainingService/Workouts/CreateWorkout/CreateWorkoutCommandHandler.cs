using TrainingService.Common.Interfaces;
using TrainingService.Exercises.Common.Repository;
using TrainingService.Workouts.Common;
using TrainingService.Workouts.Common.Repository;

namespace TrainingService.Workouts.CreateWorkout;

/// <summary>
/// Handler para o comando de criação de treino.
/// </summary>
/// <param name="repository"></param>
/// <param name="exerciseRepository"></param>
public class CreateWorkoutCommandHandler(IWorkoutRepository repository, IExerciseRepository exerciseRepository)
    : IHandler<WorkoutDto, CreateWorkoutCommand>
{
    /// <summary>
    /// Método para tratar o comando de criação de treino.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<WorkoutDto> HandleAsync(CreateWorkoutCommand command, CancellationToken cancellationToken)
    {
        Workout workout = new Workout(command.GetCreatorId(), command.GetUserId(), command.Name, command.Visibility,
            command.RestingTimeInSeconds);

        if (command.Exercises.Any())
        {
            var exercises =
                (await exerciseRepository.GetExercisesByIdsAsync(command.Exercises.ToList(), cancellationToken, true))
                .ToList();

            workout.AddWorkoutExercises(exercises);
        }
        else
            throw new ArgumentException("At least one exercise must be provided to create a workout.",
                nameof(command.Exercises));

        await repository.AddAsync(workout, cancellationToken);

        return new WorkoutDto(workout);
    }
}