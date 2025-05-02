using TrainingService.Common.Interfaces;
using TrainingService.WorkoutSessions.Common.Repository;

namespace TrainingService.WorkoutSessions.DeleteWorkoutSessionById;

public class DeleteWorkoutSessionByIdCommandHandler(IWorkoutSessionMongoRepository repository)
    : IHandler<bool, DeleteWorkoutSessionByIdCommand>
{
    public async Task<bool> HandleAsync(DeleteWorkoutSessionByIdCommand command, CancellationToken cancellationToken)
    {
        await repository.DeleteWorkoutSessionByIdAsync(command.SessionId, cancellationToken);
        
        return true;
    }
}