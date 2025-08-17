using System.ComponentModel.DataAnnotations;
using TrainingService.Exercises;
using TrainingService.Workouts.Common.Enums;
using TrainingService.Workouts.UpdateWorkoutById;

namespace TrainingService.Workouts;

public class Workout
{
    protected Workout()
    {
    }

    public Workout(string creatorId, string userId, string name, EWorkoutVisibility visibility)
    {
        CreatorId = creatorId;
        UserId = userId;
        Name = name;
        Visibility = visibility;
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

    public void ChangeVisibility(EWorkoutVisibility visibility)
    {
        Visibility = visibility;
        UpdatedAt = DateTime.Now;
    }

    public void AddExercise(Exercise exercise)
    {
        Exercises.Add(exercise);
        UpdatedAt = DateTime.Now;
    }

    public void RemoveExercise(Exercise exercise)
    {
        Exercises.Remove(exercise);
        UpdatedAt = DateTime.Now;
    }

    public void UpdateWorkout(UpdateWorkoutByIdCommand command)
    {
        Name = command.Name ?? Name;
        Visibility = command.Visibility;
        UpdatedAt = DateTime.Now;
    }

    public void AddWorkoutExercises(List<Exercise> exercises)
    {
        Exercises = exercises ?? new List<Exercise>();
    }

    public void UpdateWorkoutExercises(List<Exercise> exercises)
    {
        Exercises.Clear();
        foreach (var exercise in exercises)
            Exercises.Add(exercise);
        UpdatedAt = DateTime.Now;
    }
}