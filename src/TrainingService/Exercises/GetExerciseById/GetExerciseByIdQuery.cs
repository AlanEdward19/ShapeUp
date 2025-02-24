namespace TrainingService.Exercises.GetExerciseById;

/// <summary>
/// Query para obter um exercício por Id.
/// </summary>
public class GetExerciseByIdQuery
{
    /// <summary>
    /// Id do exercício.
    /// </summary>
    public Guid Id { get; set; }
}