namespace TrainingService.Workouts.GetWorkoutsByUserId;

/// <summary>
/// Query para obter treinos por id do usuário.
/// </summary>
public class GetWorkoutsByUserIdQuery
{
    /// <summary>
    /// Id do usuário.
    /// </summary>
    public Guid UserId { get; set; }
}