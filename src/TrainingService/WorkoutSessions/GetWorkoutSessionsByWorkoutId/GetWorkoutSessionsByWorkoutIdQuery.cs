namespace TrainingService.WorkoutSessions.GetWorkoutSessionsByWorkoutId;

public class GetWorkoutSessionsByWorkoutIdQuery(Guid workoutId)
{
    public Guid WorkoutId { get; private set; } = workoutId;
}