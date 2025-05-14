using TrainingService.Common.Interfaces;
using TrainingService.WorkoutSessions.Common.Enums;
using TrainingService.WorkoutSessions.Common.Repository;

namespace TrainingService.WorkoutSessions.CreateWorkoutSession;

public class CreateWorkoutSessionCommandHandler(IWorkoutSessionMongoRepository repository)
    : IHandler<bool, CreateWorkoutSessionCommand>
{
    public async Task<bool> HandleAsync(CreateWorkoutSessionCommand command, CancellationToken cancellationToken)
    {
        WorkoutSession workoutSession = new WorkoutSession(command.UserId, command.WorkoutId, EWorkoutStatus.InProgress, command.Exercises);
        
        await repository.CreateWorkoutSessionAsync(workoutSession, cancellationToken);
        
        return true;
    }
}