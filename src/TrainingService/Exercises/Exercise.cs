using System.ComponentModel.DataAnnotations;
using TrainingService.Exercises.Common.Enums;
using TrainingService.Exercises.UpdateExercise;

namespace TrainingService.Exercises;

public class Exercise
{
    protected Exercise() {}

    public Exercise(Guid id, string name, EMuscleGroup muscleGroups, bool? requiresWeight, string? imageUrl,
        string? videoUrl, DateTime createdAt, DateTime updatedAt)
    {
        Id = id;
        Name = name;
        MuscleGroups = muscleGroups;
        RequiresWeight = requiresWeight;
        ImageUrl = imageUrl;
        VideoUrl = videoUrl;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
    
    /// <summary>
    /// Identificador do exercício.
    /// </summary>
    [Key]
    public Guid Id { get; private set; }

    /// <summary>
    /// Nome do exercício.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Grupos musculares ao qual o exercício pertence.
    /// </summary>
    public EMuscleGroup MuscleGroups { get; private set; }

    /// <summary>
    /// Se o exercício requer peso.
    /// </summary>
    public bool? RequiresWeight { get; private set; }

    /// <summary>
    /// Imagem do exercício.
    /// </summary>
    public string? ImageUrl { get; private set; }
    
    /// <summary>
    /// Vídeo do exercício.
    /// </summary>
    public string? VideoUrl { get; private set; }
    
    /// <summary>
    /// Data de criação do exercício.
    /// </summary>
    public DateTime CreatedAt { get; init; }
    
    /// <summary>
    /// Data de atualização do exercício.
    /// </summary>
    public DateTime UpdatedAt { get; private set; }

    public void UpdateExercise(UpdateExerciseCommand exercise)
    {
        Name = exercise.Name ?? Name;
        MuscleGroups = exercise.MuscleGroups!.Aggregate(EMuscleGroup.Chest, (acc, musc) => acc | musc);
        RequiresWeight = exercise.RequiresWeight ?? RequiresWeight;
        ImageUrl = exercise.ImageUrl ?? ImageUrl;
        VideoUrl = exercise.VideoUrl ?? VideoUrl;
        UpdatedAt = DateTime.Now;
    }
}