using TrainingService.Common.Interfaces;
using TrainingService.Exercises.Common;
using TrainingService.Exercises.Common.Enums;
using TrainingService.Exercises.Common.Repository;

namespace TrainingService.Exercises.CreateExercise;

/// <summary>
///     Handler para criar um exercício
/// </summary>
/// <param name="repository"></param>
public class CreateExerciseCommandHandler(IExerciseRepository repository)
    : IHandler<ExerciseDto, CreateExerciseCommand>
{
    /// <summary>
    /// Método para criar um exercício.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<ExerciseDto> HandleAsync(CreateExerciseCommand command, CancellationToken cancellationToken)
    {
        var muscleGroups = command.MuscleGroups?.ToList() ?? new List<EMuscleGroup>();

        if (!muscleGroups.Any())
            throw new ArgumentException("At least one muscle group must be provided.");

        var combinedMuscleGroups = muscleGroups.Aggregate((acc, musc) => acc | musc);

        var exercise = new Exercise(
            Guid.NewGuid(),
            command.Name,
            combinedMuscleGroups,
            command.RequiresWeight,
            null,
            null,
            DateTime.Now,
            DateTime.Now
        );

        await repository.AddAsync(exercise, cancellationToken);
        
        return new ExerciseDto(exercise);
    }
}