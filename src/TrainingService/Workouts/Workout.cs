using System.ComponentModel.DataAnnotations;
using TrainingService.Exercises;
using TrainingService.Workouts.Common.Enums;
using TrainingService.Workouts.UpdateWorkoutById;

namespace TrainingService.Workouts;

/// <summary>
/// Entidade que representa um treino.
/// </summary>
public class Workout
{
    /// <summary>
    /// Construtor padrão para o EF Core.
    /// </summary>
    protected Workout()
    {
    }

    /// <summary>
    /// Construtor para criar um novo treino.
    /// </summary>
    /// <param name="creatorId"></param>
    /// <param name="userId"></param>
    /// <param name="name"></param>
    /// <param name="visibility"></param>
    /// <param name="restingTimeInSeconds"></param>
    public Workout(string creatorId, string userId, string name, EWorkoutVisibility visibility, int restingTimeInSeconds)
    {
        CreatorId = creatorId;
        UserId = userId;
        Name = name;
        Visibility = visibility;
        RestingTimeInSeconds = restingTimeInSeconds;
    }

    /// <summary>
    /// Id do treino.
    /// </summary>
    [Key]
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <summary>
    /// Id do usuário que criou o treino.
    /// </summary>
    public string CreatorId { get; private set; }

    /// <summary>
    /// Id do usuário para o qual o treino foi criado.
    /// </summary>
    public string UserId { get; private set; }

    /// <summary>
    /// Nome do treino.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Visibilidade do treino.
    /// </summary>
    public EWorkoutVisibility Visibility { get; private set; } = EWorkoutVisibility.Public;
    
    /// <summary>
    /// Tempo de descanso entre os exercícios do treino, em segundos.
    /// </summary>
    public int RestingTimeInSeconds { get; set; }

    /// <summary>
    /// Exercícios do treino.
    /// </summary>
    public virtual ICollection<Exercise> Exercises { get; private set; } = new List<Exercise>();

    /// <summary>
    /// Data de criação do treino.
    /// </summary>
    public DateTime CreatedAt { get; init; } = DateTime.Now;

    /// <summary>
    /// Data de atualização do treino.
    /// </summary>
    public DateTime UpdatedAt { get; private set; } = DateTime.Now;

    /// <summary>
    /// Método para atualizar o treino com base em um comando.
    /// </summary>
    /// <param name="command"></param>
    public void UpdateWorkout(UpdateWorkoutByIdCommand command)
    {
        Name = command.Name;
        Visibility = command.Visibility;
        UpdatedAt = DateTime.Now;
        RestingTimeInSeconds = command.RestingTimeInSeconds;
    }

    /// <summary>
    /// Método para adicionar exercícios ao treino.
    /// </summary>
    /// <param name="exercises"></param>
    public void AddWorkoutExercises(List<Exercise> exercises)
    {
        Exercises = exercises ?? new List<Exercise>();
    }

    /// <summary>
    /// Método para atualizar os exercícios do treino, substituindo os existentes.
    /// </summary>
    /// <param name="exercises"></param>
    public void UpdateWorkoutExercises(List<Exercise> exercises)
    {
        Exercises.Clear();
        foreach (var exercise in exercises)
            Exercises.Add(exercise);
        UpdatedAt = DateTime.Now;
    }
}