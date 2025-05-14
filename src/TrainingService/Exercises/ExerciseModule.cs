using TrainingService.Common.Interfaces;
using TrainingService.Exercises.Common;
using TrainingService.Exercises.Common.Repository;
using TrainingService.Exercises.CreateExercise;
using TrainingService.Exercises.DeleteExerciseById;
using TrainingService.Exercises.GetExerciseById;
using TrainingService.Exercises.GetExerciseByMuscleGroup;
using TrainingService.Exercises.UpdateExercise;

namespace TrainingService.Exercises;

/// <summary>
/// Modulo para resolver as dependências relacionadas a exercícios
/// </summary>
public static class ExerciseModule
{
    /// <summary>
    /// Método para resolver as dependências relacionadas a exercícios
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureExerciseRelatedDependencies(this IServiceCollection services)
    {
        services
            .AddRepositories()
            .AddHandlers();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IExerciseRepository, ExerciseRepository>();

        return services; 
    }
    
    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<IHandler<bool, CreateExerciseCommand>, CreateExerciseCommandHandler>();
        services.AddScoped<IHandler<bool, DeleteExerciseByIdCommand>, DeleteExerciseCommandHandler>();
        services.AddScoped<IHandler<ICollection<ExerciseDto>, GetExerciseByMuscleGroupQuery>, GetExerciseByMuscleGroupQueryHandler>();
        services.AddScoped<IHandler<ExerciseDto, GetExerciseByIdQuery>, GetExerciseByIdQueryHandler>();
        services.AddScoped<IHandler<bool, UpdateExerciseCommand>, UpdateExerciseCommandHandler>();
        return services;
    }
}