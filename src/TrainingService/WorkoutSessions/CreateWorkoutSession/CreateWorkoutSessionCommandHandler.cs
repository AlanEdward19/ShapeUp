using TrainingService.Common.Interfaces;
using TrainingService.Exercises;
using TrainingService.Exercises.Common.Repository;
using TrainingService.WorkoutSessions.Common.Enums;
using TrainingService.WorkoutSessions.Common.Repository;

namespace TrainingService.WorkoutSessions.CreateWorkoutSession;

public class CreateWorkoutSessionCommandHandler(
    IWorkoutSessionMongoRepository repository,
    IExerciseRepository exerciseRepository)
    : IHandler<WorkoutSessionDto, CreateWorkoutSessionCommand>
{
    public async Task<WorkoutSessionDto> HandleAsync(CreateWorkoutSessionCommand command,
        CancellationToken cancellationToken)
    {
        WorkoutSession workoutSession = new WorkoutSession(command.GetUserId(), command.WorkoutId,
            EWorkoutStatus.InProgress, command.Exercises);

        await repository.CreateWorkoutSessionAsync(workoutSession, cancellationToken);

        var exerciseIds = command.Exercises.Select(e => Guid.Parse(e.ExerciseId)).ToList();

        var exercises = exerciseIds.Any() ? await exerciseRepository.GetExercisesByIdsAsync(exerciseIds, cancellationToken) : [];

        return new(workoutSession, exercises);
    }
}