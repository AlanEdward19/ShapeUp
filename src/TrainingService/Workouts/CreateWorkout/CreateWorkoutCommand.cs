using TrainingService.Workouts.Common.Enums;

namespace TrainingService.Workouts.CreateWorkout;

/// <summary>
/// Comando para criar um treino.
/// </summary>
/// <param name="name"></param>
/// <param name="visibility"></param>
/// <param name="exercises"></param>
public class CreateWorkoutCommand(string name, EWorkoutVisibility visibility, IEnumerable<Guid> exercises)
{
    /// <summary>
    /// Id do usuário criador do treino.
    /// </summary>
    private string CreatorId { get; set; }
    
    /// <summary>
    /// Método para definir o Id do usuário criador do treino.
    /// </summary>
    /// <param name="creatorId"></param>
    public void SetCreatorId(string creatorId) => CreatorId = creatorId;
    
    /// <summary>
    /// Método para obter o Id do usuário criador do treino.
    /// </summary>
    /// <returns></returns>
    public string GetCreatorId() => CreatorId;
    
    /// <summary>
    /// Id do usuário para o qual o treino foi criado. Caso não seja informado, será o usuário autenticado.
    /// </summary>
    private string? UserId { get; set; }
    
    /// <summary>
    /// Método para definir o Id do usuário para o qual o treino foi criado.
    /// </summary>
    /// <param name="userId"></param>
    public void SetUserId(string? userId) => UserId = userId;
    
    /// <summary>
    /// Método para obter o Id do usuário para o qual o treino foi criado.
    /// </summary>
    /// <returns></returns>
    public string? GetUserId() => UserId;

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