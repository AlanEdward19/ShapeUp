using TrainingService.Common.Interfaces;
using TrainingService.Exercises.Common.Repository;
using TrainingService.WorkoutSessions.Common.Repository;

namespace TrainingService.WorkoutSessions.GetWorkoutSessionById;

public class GetWorkoutSessionByIdQueryHandler(
    IWorkoutSessionMongoRepository repository,
    IExerciseRepository exerciseRepository) : IHandler<WorkoutSessionDto, GetWorkoutSessionByIdQuery>
{
    public async Task<WorkoutSessionDto> HandleAsync(GetWorkoutSessionByIdQuery query,
        CancellationToken cancellationToken)
    {
        WorkoutSession? workoutSession =
            await repository.GetWorkoutSessionByIdAsync(query.SessionId, cancellationToken);

        ArgumentNullException.ThrowIfNull(workoutSession, $"Workout session with ID {query.SessionId} not found.");

        var exerciseIds = workoutSession.Exercises.Select(e => Guid.Parse(e.ExerciseId)).ToList();

        var exercises = await exerciseRepository.GetExercisesByIdsAsync(exerciseIds, cancellationToken);

        return new(workoutSession, exercises);
    }
}