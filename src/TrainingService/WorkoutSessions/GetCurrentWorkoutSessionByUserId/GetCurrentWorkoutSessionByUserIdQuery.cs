namespace TrainingService.WorkoutSessions.GetCurrentWorkoutSessionByUserId;

public class GetCurrentWorkoutSessionByUserIdQuery(string userId)
{
    public string UserId { get; private set; } = userId;
}