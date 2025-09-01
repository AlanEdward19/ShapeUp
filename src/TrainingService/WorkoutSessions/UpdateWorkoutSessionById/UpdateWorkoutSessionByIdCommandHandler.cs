using SharedKernel.Exceptions;
using TrainingService.Common.Interfaces;
using TrainingService.Exercises.Common.Repository;
using TrainingService.WorkoutSessions.Common.Dto;
using TrainingService.WorkoutSessions.Common.Repository;

namespace TrainingService.WorkoutSessions.UpdateWorkoutSessionById;

/// <summary>
/// Handler para o comando de atualização de uma sessão de treino por ID.
/// </summary>
/// <param name="repository"></param>
/// <param name="exerciseRepository"></param>
public class UpdateWorkoutSessionByIdCommandHandler(IWorkoutSessionMongoRepository repository, IExerciseRepository exerciseRepository)
    : IHandler<WorkoutSessionDto, UpdateWorkoutSessionByIdCommand>
{
    /// <summary>
    /// Handler para o comando de atualização de uma sessão de treino por ID.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<WorkoutSessionDto> HandleAsync(UpdateWorkoutSessionByIdCommand command, CancellationToken cancellationToken)
    {
        WorkoutSession? workoutSession = await repository.GetWorkoutSessionByIdAsync(command.GetSessionId(), cancellationToken);
        
        if(workoutSession == null)
            throw new NotFoundException($"Workout session with ID {command.GetSessionId()} does not exist.");

        if (workoutSession.UserId != command.GetUserId())
            throw new ForbiddenException(
                $"You are not authorized to update this workout session.");

        if (command.Status != null)
            workoutSession.UpdateStatus(command.Status.Value);
        
        if (command.Exercises != null)
            workoutSession.UpdateExercises(command.Exercises);
        
        await repository.UpdateWorkoutSessionByIdAsync(command.GetSessionId(), workoutSession, cancellationToken);
        
        var exerciseIds = workoutSession.Exercises.Select(e => Guid.Parse(e.ExerciseId)).ToList();
        
        var exercises = await exerciseRepository.GetExercisesByIdsAsync(exerciseIds, cancellationToken);
        
        return new(workoutSession, exercises);
    }
}