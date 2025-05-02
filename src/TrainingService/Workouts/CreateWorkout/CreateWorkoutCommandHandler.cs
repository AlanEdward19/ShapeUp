using TrainingService.Common.Interfaces;
using TrainingService.Exercises;
using TrainingService.Exercises.Common.Repository;
using TrainingService.Workouts.Common.Repository;

namespace TrainingService.Workouts.CreateWorkout;

public class CreateWorkoutCommandHandler(IWorkoutRepository repository, IExerciseRepository exerciseRepository)
    : IHandler<bool, CreateWorkoutCommand>
{
    public async Task<bool> HandleAsync(CreateWorkoutCommand command, CancellationToken cancellationToken)
    {
        Workout workout = new Workout(command.CreatorId, command.UserId, command.Name, command.Visibility);
        
        if (command.Exercises.Any())
        {
            List<Exercise> exercises = new List<Exercise>();
            foreach (var exerciseId in command.Exercises)
            {
                var exercise = await exerciseRepository.GetExerciseAsync(exerciseId, cancellationToken);
                if (exercise != null)
                    exercises.Add(exercise);
            }
            
            workout.AddWorkoutExercises(exercises);
        }
        
        await repository.AddAsync(workout, cancellationToken);
        
        return true;
    }
}