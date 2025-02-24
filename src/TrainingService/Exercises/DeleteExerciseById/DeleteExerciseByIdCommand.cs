namespace TrainingService.Exercises.DeleteExerciseById;

/// <summary>
/// Comando para deletar um exercício.
/// </summary>
/// <param name="id"></param>
public class DeleteExerciseByIdCommand(Guid id)
{
    /// <summary>
    /// Id do exercício.
    /// </summary>
    public Guid Id { get; set; } = id;
}