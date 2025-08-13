using SharedKernel.Exceptions;
using TrainingService.Common.Interfaces;
using TrainingService.Exercises.Common.Repository;
using TrainingService.WorkoutSessions.Common.Repository;

namespace TrainingService.WorkoutSessions.GetCurrentWorkoutSessionByUserId;

public class GetCurrentWorkoutSessionByUserIdQueryHandler(IWorkoutSessionMongoRepository repository, IExerciseRepository exerciseRepository) : IHandler<
    WorkoutSessionDto, GetCurrentWorkoutSessionByUserIdQuery>
{
    public async Task<WorkoutSessionDto> HandleAsync(GetCurrentWorkoutSessionByUserIdQuery query, CancellationToken cancellationToken)
    {
        var workoutSession = await repository.GetCurrentWorkoutSessionByUserIdAsync(query.UserId, cancellationToken);
        
        if (workoutSession == null)
            throw new NotFoundException($"No current workout session found for user with ID: {query.UserId}");
        
        var exerciseIds = workoutSession.Exercises.Select(e => Guid.Parse(e.ExerciseId)).ToList();

        var exercises = await exerciseRepository.GetExercisesByIdsAsync(exerciseIds, cancellationToken);

        return new(workoutSession, exercises);
    }
}