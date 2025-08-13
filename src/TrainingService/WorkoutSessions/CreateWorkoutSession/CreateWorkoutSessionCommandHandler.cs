using TrainingService.Common.Interfaces;
using TrainingService.WorkoutSessions.Common.Enums;
using TrainingService.WorkoutSessions.Common.Repository;

namespace TrainingService.WorkoutSessions.CreateWorkoutSession;

public class CreateWorkoutSessionCommandHandler(IWorkoutSessionMongoRepository repository)
    : IHandler<WorkoutSession, CreateWorkoutSessionCommand>
{
    public async Task<WorkoutSession> HandleAsync(CreateWorkoutSessionCommand command, CancellationToken cancellationToken)
    {
        WorkoutSession workoutSession = new WorkoutSession(command.GetUserId(), command.WorkoutId, EWorkoutStatus.InProgress, command.Exercises);
        
        await repository.CreateWorkoutSessionAsync(workoutSession, cancellationToken);
        
        return workoutSession;
    }
}