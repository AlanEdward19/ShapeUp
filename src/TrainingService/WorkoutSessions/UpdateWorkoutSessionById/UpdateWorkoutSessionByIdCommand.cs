using TrainingService.WorkoutSessions.Common.Enums;
using TrainingService.WorkoutSessions.Common.ValueObjects;

namespace TrainingService.WorkoutSessions.UpdateWorkoutSessionById;

/// <summary>
/// Comando para atualizar uma sessão de treino por id.
/// </summary>
/// <param name="id"></param>
/// <param name="status"></param>
/// <param name="exercises"></param>
public class UpdateWorkoutSessionByIdCommand(
    EWorkoutStatus? status,
    List<WorkoutSessionExerciseValueObject>? exercises)
{
    /// <summary>
    /// Id da sessão de treino
    /// </summary>
    private string SessionId { get; set; }
    
    /// <summary>
    /// Método para definir o id da sessão de treino
    /// </summary>
    /// <param name="id"></param>
    public void SetSessionId(string id) => SessionId = id;
    
    /// <summary>
    /// Método para obter o id da sessão de treino
    /// </summary>
    /// <returns></returns>
    public string GetSessionId() => SessionId;

    /// <summary>
    /// Status da sessão de treino
    /// </summary>
    public EWorkoutStatus? Status { get; set; } = status;

    /// <summary>
    /// Exercícios da sessão de treino
    /// </summary>
    public List<WorkoutSessionExerciseValueObject>? Exercises { get; set; } = exercises;
}