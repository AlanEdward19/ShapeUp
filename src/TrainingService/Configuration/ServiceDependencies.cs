using TrainingService.Connections;
using TrainingService.Exercises;
using TrainingService.Workouts;
using TrainingService.WorkoutSessions;

namespace TrainingService.Configuration;

/// <summary>
///     Classe para resolver as dependências de serviços
/// </summary>
public static class ServiceDependencies
{
    /// <summary>
    ///     Método para resolver as dependências de serviços
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection SolveServiceDependencies(this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .ConfigureExerciseRelatedDependencies()
            .ConfigureWorkoutRelatedDependencies()
            .ConfigureWorkoutSessionRelatedDependencies()
            .ConfigureConnections(configuration);

        return services;
    }
}