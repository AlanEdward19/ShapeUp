using TrainingService.Workouts.Common.Enums;

namespace TrainingService.Workouts.CreateWorkout;

/// <summary>
/// Comando para criar um treino.
/// </summary>
/// <param name="userId"></param>
/// <param name="name"></param>
/// <param name="visibility"></param>
/// <param name="exercises"></param>
public class CreateWorkoutCommand(Guid? userId, string name, EWorkoutVisibility visibility, IEnumerable<Guid> exercises)
{
    /// <summary>
    /// Id do usuário para o qual o treino foi criado. Caso não seja informado, será o usuário autenticado.
    /// </summary>
    public Guid? UserId { get; set; } = userId;

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