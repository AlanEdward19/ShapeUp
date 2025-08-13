using TrainingService.Common.Interfaces;
using TrainingService.WorkoutSessions.Common.Enums;
using TrainingService.WorkoutSessions.Common.Repository;

namespace TrainingService.WorkoutSessions.UpdateWorkoutSessionById;

public class UpdateWorkoutSessionByIdCommandHandler(IWorkoutSessionMongoRepository repository)
    : IHandler<WorkoutSession, UpdateWorkoutSessionByIdCommand>
{
    public async Task<WorkoutSession> HandleAsync(UpdateWorkoutSessionByIdCommand command, CancellationToken cancellationToken)
    {
        WorkoutSession? workoutSession = await repository.GetWorkoutSessionByIdAsync(command.GetSessionId(), cancellationToken);
        ArgumentNullException.ThrowIfNull(workoutSession);

        if (command.Status != null)
            workoutSession.UpdateStatus(command.Status.Value);
        
        if (command.Exercises != null)
            workoutSession.UpdateExercises(command.Exercises);
        
        await repository.UpdateWorkoutSessionByIdAsync(command.GetSessionId(), workoutSession, cancellationToken);
        
        return workoutSession;
    }
}