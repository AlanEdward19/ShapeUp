using SharedKernel.Exceptions;
using TrainingService.Common.Interfaces;
using TrainingService.WorkoutSessions.Common.Repository;

namespace TrainingService.WorkoutSessions.DeleteWorkoutSessionById;

/// <summary>
/// Handler para o comando de deleção de uma sessão de treino por ID.
/// </summary>
/// <param name="repository"></param>
public class DeleteWorkoutSessionByIdCommandHandler(IWorkoutSessionMongoRepository repository)
    : IHandler<bool, DeleteWorkoutSessionByIdCommand>
{
    /// <summary>
    /// Método para lidar com o comando de deleção de uma sessão de treino por ID.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    /// <exception cref="ForbiddenException"></exception>
    public async Task<bool> HandleAsync(DeleteWorkoutSessionByIdCommand command, CancellationToken cancellationToken)
    {
        WorkoutSession? workoutSession = await repository.GetWorkoutSessionByIdAsync(command.SessionId, cancellationToken);
        
        if (workoutSession == null)
            throw new NotFoundException($"Workout session with ID {command.SessionId} does not exist.");
        
        if(workoutSession.UserId != command.UserId)
            throw new ForbiddenException($"User {command.UserId} is not authorized to delete this workout session.");
        
        await repository.DeleteWorkoutSessionByIdAsync(command.SessionId, cancellationToken);
        
        return true;
    }
}