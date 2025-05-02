using TrainingService.Workouts.Common.Enums;

namespace TrainingService.Workouts.UpdateWorkoutById;

/// <summary>
/// Comando para atualizar um treino por id.
/// </summary>
/// <param name="id"></param>
/// <param name="name"></param>
/// <param name="visibility"></param>
/// <param name="exercises"></param>
public class UpdateWorkoutByIdCommand(Guid id, Guid userId, string name, EWorkoutVisibility visibility, IEnumerable<Guid> exercises)
{
    /// <summary>
    /// Id do treino.
    /// </summary>
    public Guid Id { get; set; } = id;
    
    /// <summary>
    /// Id usuario do treino.
    /// </summary>
    public Guid UserId { get; set; } = userId;
    
    /// <summary>
    /// Nome do treino.
    /// </summary>
    public string Name { get; set; } = name;

    /// <summary>
    /// Visibilidade do treino.
    /// </summary>
    public EWorkoutVisibility Visibility { get; set; } = visibility;

    /// <summary>
    /// Ids dos exercícios do treino.
    /// </summary>
    public IEnumerable<Guid> Exercises { get; set; } = exercises;
}