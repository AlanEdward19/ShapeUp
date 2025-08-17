using TrainingService.Workouts.Common.Enums;

namespace TrainingService.Workouts.UpdateWorkoutById;

/// <summary>
/// Comando para atualizar um treino por id.
/// </summary>
/// <param name="id"></param>
/// <param name="name"></param>
/// <param name="visibility"></param>
/// <param name="exercises"></param>
public class UpdateWorkoutByIdCommand(string name, EWorkoutVisibility visibility, IEnumerable<Guid> exercises, int restingTimeInSeconds)
{
    /// <summary>
    /// Id do treino.
    /// </summary>
    private Guid Id { get; set; }
    
    public void SetWorkoutId(Guid id) => Id = id;
    
    public Guid GetWorkoutId() => Id;
    
    private string UserId { get; set; }
    
    public void SetUserId(string userId) => UserId = userId;
    
    public string GetUserId() => UserId;
    
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
    
    /// <summary>
    /// Tempo de descanso entre os exercícios, em segundos.
    /// </summary>
    public int RestingTimeInSeconds { get; set; } = restingTimeInSeconds;
}