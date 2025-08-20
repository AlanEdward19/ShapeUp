using TrainingService.Common.Interfaces;
using TrainingService.Exercises.Common.Repository;
using TrainingService.Workouts.Common.Repository;
using TrainingService.WorkoutSessions.Common.Dto;
using TrainingService.WorkoutSessions.Common.Enums;
using TrainingService.WorkoutSessions.Common.Repository;

namespace TrainingService.WorkoutSessions.CreateWorkoutSession;

/// <summary>
/// Handler para o comando de criação de uma sessão de treino.
/// </summary>
/// <param name="repository"></param>
/// <param name="workoutRepository"></param>
/// <param name="exerciseRepository"></param>
public class CreateWorkoutSessionCommandHandler(
    IWorkoutSessionMongoRepository repository,
    IWorkoutRepository workoutRepository,
    IExerciseRepository exerciseRepository)
    : IHandler<WorkoutSessionDto, CreateWorkoutSessionCommand>
{
    /// <summary>
    /// Método para lidar com o comando de criação de uma sessão de treino.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<WorkoutSessionDto> HandleAsync(CreateWorkoutSessionCommand command,
        CancellationToken cancellationToken)
    {
        if (!await workoutRepository.WorkoutExistsAsync(command.WorkoutId, cancellationToken))
            throw new ArgumentException($"Workout with ID {command.WorkoutId} does not exist.");

        List<Guid> exerciseIds = new(command.Exercises.Count);

        foreach (var exercise in command.Exercises.Distinct())
        {
            if(Guid.TryParse(exercise.ExerciseId, out Guid exerciseId) && exerciseId != Guid.Empty)
                exerciseIds.Add(exerciseId);
            else
                throw new ArgumentException($"Invalid Exercise ID: {exercise.ExerciseId}");
        }
        
        await exerciseRepository.ExerciseExistsAsync(exerciseIds, cancellationToken);

        WorkoutSession workoutSession = new WorkoutSession(command.GetUserId(), command.WorkoutId,
            EWorkoutStatus.InProgress, command.Exercises);

        await repository.CreateWorkoutSessionAsync(workoutSession, cancellationToken);

        var exercises = await exerciseRepository.GetExercisesByIdsAsync(exerciseIds, cancellationToken);

        return new(workoutSession, exercises);
    }
}