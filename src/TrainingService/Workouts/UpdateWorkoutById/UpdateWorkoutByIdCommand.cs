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
    
    /// <summary>
    /// Método para definir o Id do treino.
    /// </summary>
    /// <param name="id"></param>
    public void SetWorkoutId(Guid id) => Id = id;
    
    /// <summary>
    /// Método para obter o Id do treino.
    /// </summary>
    /// <returns></returns>
    public Guid GetWorkoutId() => Id;
    
    private string UserId { get; set; }
    
    /// <summary>
    /// Método para definir o Id do usuário que está atualizando o treino.
    /// </summary>
    /// <param name="userId"></param>
    public void SetUserId(string userId) => UserId = userId;
    
    /// <summary>
    /// Método para obter o Id do usuário que está atualizando o treino.
    /// </summary>
    /// <returns></returns>
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