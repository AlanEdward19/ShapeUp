using TrainingService.Common.Interfaces;
using TrainingService.Exercises;
using TrainingService.Exercises.Common.Repository;
using TrainingService.Workouts.Common;
using TrainingService.Workouts.Common.Repository;

namespace TrainingService.Workouts.CreateWorkout;

public class CreateWorkoutCommandHandler(IWorkoutRepository repository, IExerciseRepository exerciseRepository)
    : IHandler<WorkoutDto, CreateWorkoutCommand>
{
    public async Task<WorkoutDto> HandleAsync(CreateWorkoutCommand command, CancellationToken cancellationToken)
    {
        Workout workout = new Workout(command.GetCreatorId(), command.GetUserId(), command.Name, command.Visibility);

        if (command.Exercises.Any())
        {
            var exercises =
                (await exerciseRepository.GetExercisesByIdsAsync(command.Exercises.ToList(), cancellationToken))
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