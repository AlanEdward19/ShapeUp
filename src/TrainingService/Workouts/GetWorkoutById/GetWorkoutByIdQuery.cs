namespace TrainingService.Workouts.GetWorkoutById;

/// <summary>
/// Query para obter um treino por id.
/// </summary>
public class GetWorkoutByIdQuery
{
    /// <summary>
    /// Id do treino.
    /// </summary>
    public Guid Id { get; set; }
}