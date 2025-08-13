using TrainingService.Common.Interfaces;
using TrainingService.Exercises.Common.Repository;
using TrainingService.WorkoutSessions.Common.Repository;
using TrainingService.WorkoutSessions.GetWorkoutSessionByUserId;

namespace TrainingService.WorkoutSessions.GetWorkoutSessionById;

public class GetWorkoutSessionsByUserIdQueryHandler(
    IWorkoutSessionMongoRepository repository,
    IExerciseRepository exerciseRepository) : IHandler<ICollection<WorkoutSessionDto>, GetWorkoutSessionsByUserIdQuery>
{
    public async Task<ICollection<WorkoutSessionDto>> HandleAsync(GetWorkoutSessionsByUserIdQuery query,
        CancellationToken cancellationToken)
    {
        var workoutSessions = await repository.GetWorkoutSessionsByUserIdAsync(query.UserId, cancellationToken);
        List<WorkoutSessionDto> workoutSessionDtos = new(workoutSessions.Count);
        var exerciseIds = workoutSessions.SelectMany(x => x.Exercises.Select(e => Guid.Parse(e.ExerciseId)))
            .ToList();

        var exercises = await exerciseRepository.GetExercisesByIdsAsync(exerciseIds, cancellationToken);

        foreach (var workoutSession in workoutSessions)
        {
            var workoutExerciseIds = workoutSession
                .Exercises.Select(e => Guid.Parse(e.ExerciseId))
                .ToList();
            var workoutExercises = exercises.Where(x => workoutExerciseIds.Contains(x.Id)).ToList();
            workoutSessionDtos.Add(new WorkoutSessionDto(workoutSession, workoutExercises));
        }

        return workoutSessionDtos;
    }
}