using TrainingService.Common.Interfaces;
using TrainingService.Exercises.Common.Repository;
using TrainingService.WorkoutSessions.Common.Dto;
using TrainingService.WorkoutSessions.Common.Repository;

namespace TrainingService.WorkoutSessions.GetWorkoutSessionsByUserId;

/// <summary>
/// Handler para a consulta de sessões de treino por ID de usuário.
/// </summary>
/// <param name="repository"></param>
/// <param name="exerciseRepository"></param>
public class GetWorkoutSessionsByUserIdQueryHandler(
    IWorkoutSessionMongoRepository repository,
    IExerciseRepository exerciseRepository) : IHandler<ICollection<WorkoutSessionDto>, GetWorkoutSessionsByUserIdQuery>
{
    /// <summary>
    /// Método para lidar com a consulta de sessões de treino por ID de usuário.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
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