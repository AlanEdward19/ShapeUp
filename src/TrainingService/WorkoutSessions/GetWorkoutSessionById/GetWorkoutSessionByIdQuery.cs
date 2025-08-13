namespace TrainingService.WorkoutSessions.GetWorkoutSessionById;

public class GetWorkoutSessionByIdQuery(string sessionId)
{
    public string SessionId { get; private set; } = sessionId;
}