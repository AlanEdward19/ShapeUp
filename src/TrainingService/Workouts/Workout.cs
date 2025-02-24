using TrainingService.Exercises;
using TrainingService.Workouts.Common.Enums;

namespace TrainingService.Workouts;

public class Workout
{
    /// <summary>
    /// Id do treino.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Id do usuário que criou o treino.
    /// </summary>
    public Guid CreatorId { get; set; }
    
    /// <summary>
    /// Id do usuário para o qual o treino foi criado.
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Nome do treino.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Visibilidade do treino.
    /// </summary>
    public EWorkoutVisibility Visibility { get; set; }
    
    /// <summary>
    /// Exercícios do treino.
    /// </summary>
    public virtual ICollection<Exercise> Exercises { get; set; }
    
    /// <summary>
    /// Data de criação do treino.
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Data de atualização do treino.
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}