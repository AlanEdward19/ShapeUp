namespace TrainingService.Workouts.DeleteWorkoutById;

/// <summary>
/// Comando para deletar um treino por id.
/// </summary>
/// <param name="id"></param>
public class DeleteWorkoutByIdCommand(Guid id)
{
    /// <summary>
    /// Id do treino.
    /// </summary>
    public Guid Id { get; set; } = id;
}