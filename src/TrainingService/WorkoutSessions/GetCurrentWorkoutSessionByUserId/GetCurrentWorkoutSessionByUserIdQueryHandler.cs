using SharedKernel.Exceptions;
using TrainingService.Common.Interfaces;
using TrainingService.Exercises.Common.Repository;
using TrainingService.WorkoutSessions.Common.Dto;
using TrainingService.WorkoutSessions.Common.Repository;

namespace TrainingService.WorkoutSessions.GetCurrentWorkoutSessionByUserId;

/// <summary>
/// Handler para a consulta de uma sessão de treino atual por ID de usuário.
/// </summary>
/// <param name="repository"></param>
/// <param name="exerciseRepository"></param>
public class GetCurrentWorkoutSessionByUserIdQueryHandler(IWorkoutSessionMongoRepository repository, IExerciseRepository exerciseRepository) : IHandler<
    WorkoutSessionDto, GetCurrentWorkoutSessionByUserIdQuery>
{
    /// <summary>
    /// Método para lidar com a consulta de uma sessão de treino atual por ID de usuário.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
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