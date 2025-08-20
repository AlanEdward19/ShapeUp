namespace TrainingService.Workouts.GetWorkoutsByUserId;

/// <summary>
/// Query para obter treinos por id do usuário.
/// </summary>
public class GetWorkoutsByUserIdQuery
{
    /// <summary>
    /// Id do usuário.
    /// </summary>
    public string UserId { get; private set; }
    
    /// <summary>
    /// Método para id do usuário.
    /// </summary>
    /// <param name="userId"></param>
    public void SetUserId(string userId)
    {
        UserId = userId;
    }
}