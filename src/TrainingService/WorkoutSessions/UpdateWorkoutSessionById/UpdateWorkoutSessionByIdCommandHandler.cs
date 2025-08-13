using TrainingService.Common.Interfaces;
using TrainingService.Exercises.Common.Repository;
using TrainingService.WorkoutSessions.Common.Enums;
using TrainingService.WorkoutSessions.Common.Repository;

namespace TrainingService.WorkoutSessions.UpdateWorkoutSessionById;

public class UpdateWorkoutSessionByIdCommandHandler(IWorkoutSessionMongoRepository repository, IExerciseRepository exerciseRepository)
    : IHandler<WorkoutSessionDto, UpdateWorkoutSessionByIdCommand>
{
    public async Task<WorkoutSessionDto> HandleAsync(UpdateWorkoutSessionByIdCommand command, CancellationToken cancellationToken)
    {
        WorkoutSession? workoutSession = await repository.GetWorkoutSessionByIdAsync(command.GetSessionId(), cancellationToken);
        ArgumentNullException.ThrowIfNull(workoutSession);

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