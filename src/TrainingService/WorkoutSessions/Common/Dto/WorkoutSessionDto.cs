using TrainingService.Exercises;
using TrainingService.WorkoutSessions.Common.Enums;

namespace TrainingService.WorkoutSessions;

public class WorkoutSessionDto(WorkoutSession workoutSession, ICollection<Exercise> exercises)
{
    public string SessionId { get; private set; } = workoutSession.SessionId;
    
    public string UserId { get; private set; } = workoutSession.UserId;
    
    public string WorkoutId { get; private set; } = workoutSession.WorkoutId;
    
    public DateTime StartedAt { get; private set; } = workoutSession.StartedAt;
    
    public DateTime? EndedAt { get; private set; } = workoutSession.EndedAt;
    
    public EWorkoutStatus Status { get; private set; } = workoutSession.Status;
    
    public List<WorkoutSessionExerciseDto> Exercises { get; private set; } = workoutSession.Exercises.Zip(exercises)
        .Select(pair => new WorkoutSessionExerciseDto(pair.First, pair.Second))
        .ToList();
}