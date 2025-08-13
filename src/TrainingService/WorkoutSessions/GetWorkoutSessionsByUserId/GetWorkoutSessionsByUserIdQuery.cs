namespace TrainingService.WorkoutSessions.GetWorkoutSessionByUserId;

public class GetWorkoutSessionsByUserIdQuery(string userId)
{
    public string UserId { get; private set; } = userId;
}