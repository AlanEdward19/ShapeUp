using TrainingService.Common.Interfaces;
using TrainingService.WorkoutSessions.Common.Enums;
using TrainingService.WorkoutSessions.Common.Repository;

namespace TrainingService.WorkoutSessions.UpdateWorkoutSessionById;

public class UpdateWorkoutSessionByIdCommandHandler(IWorkoutSessionMongoRepository repository)
    : IHandler<bool, UpdateWorkoutSessionByIdCommand>
{
    public async Task<bool> HandleAsync(UpdateWorkoutSessionByIdCommand command, CancellationToken cancellationToken)
    {
        WorkoutSession? workoutSession = await repository.GetWorkoutSessionByIdAsync(command.SessionId.ToString(), cancellationToken);
        ArgumentNullException.ThrowIfNull(workoutSession);

        if (command.Status != null)
        {
            workoutSession.UpdateStatus(command.Status.Value);
        }
        
        if (command.Exercises != null)
        {
            workoutSession.UpdateExercises(command.Exercises);
        }
        
        await repository.UpdateWorkoutSessionByIdAsync(command.SessionId, workoutSession, cancellationToken);
        
        return true;
    }
}