using SharedKernel.Exceptions;
using TrainingService.Common.Interfaces;
using TrainingService.Exercises.Common.Repository;
using TrainingService.Workouts.Common.Repository;
using TrainingService.WorkoutSessions.Common.Dto;
using TrainingService.WorkoutSessions.Common.Repository;

namespace TrainingService.WorkoutSessions.GetWorkoutSessionsByWorkoutId;

/// <summary>
/// Handler para a consulta de sessões de treino por ID de treino.
/// </summary>
/// <param name="repository"></param>
/// <param name="workoutRepository"></param>
/// <param name="exerciseRepository"></param>
public class GetWorkoutSessionsByWorkoutIdQueryHandler(
    IWorkoutSessionMongoRepository repository,
    IWorkoutRepository workoutRepository,
    IExerciseRepository exerciseRepository) : IHandler<ICollection<WorkoutSessionDto>, GetWorkoutSessionsByWorkoutIdQuery>
{
    /// <summary>
    /// Método para lidar com a consulta de sessões de treino por ID de treino.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ICollection<WorkoutSessionDto>> HandleAsync(GetWorkoutSessionsByWorkoutIdQuery query,
        CancellationToken cancellationToken)
    {
        if(!await workoutRepository.WorkoutExistsAsync(query.WorkoutId, cancellationToken))
            throw new NotFoundException($"Workout with ID {query.WorkoutId} does not exist.");
        
        var workoutSessions = await repository.GetWorkoutSessionsByWorkoutIdIdAsync(query.WorkoutId, cancellationToken);
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