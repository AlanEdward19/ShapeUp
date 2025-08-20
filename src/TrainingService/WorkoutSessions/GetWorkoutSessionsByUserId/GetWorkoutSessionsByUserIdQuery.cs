namespace TrainingService.WorkoutSessions.GetWorkoutSessionsByUserId;

/// <summary>
/// Query para obter todas as sessões de treino de um usuário pelo ID do usuário.
/// </summary>
/// <param name="userId"></param>
public class GetWorkoutSessionsByUserIdQuery(string userId)
{
    /// <summary>
    /// Id do usuário cujas sessões de treino serão obtidas.
    /// </summary>
    public string UserId { get; private set; } = userId;
}