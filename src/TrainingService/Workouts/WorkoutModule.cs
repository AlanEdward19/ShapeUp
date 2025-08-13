using TrainingService.Common.Interfaces;
using TrainingService.Workouts.Common;
using TrainingService.Workouts.Common.Repository;
using TrainingService.Workouts.CreateWorkout;
using TrainingService.Workouts.DeleteWorkoutById;
using TrainingService.Workouts.GetWorkoutById;
using TrainingService.Workouts.GetWorkoutsByUserId;
using TrainingService.Workouts.UpdateWorkoutById;

namespace TrainingService.Workouts;

/// <summary>
/// Modulo para resolver as dependências relacionadas a treinos
/// </summary>
public static class WorkoutModule
{
    /// <summary>
    /// Método para resolver as dependências relacionadas a treinos
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureWorkoutRelatedDependencies(this IServiceCollection services)
    {
        services
            .AddRepositories()
            .AddHandlers();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IWorkoutRepository, WorkoutRepository>();

        return services; 
    }
    
    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<IHandler<WorkoutDto, CreateWorkoutCommand>, CreateWorkoutCommandHandler>();
        services.AddScoped<IHandler<bool, DeleteWorkoutByIdCommand>, DeleteWorkoutByIdCommandHandler>();
        services.AddScoped<IHandler<ICollection<WorkoutDto>, GetWorkoutsByUserIdQuery>, GetWorkoutsByUserIdQueryHandler>();
        services.AddScoped<IHandler<WorkoutDto, GetWorkoutByIdQuery>, GetWorkoutByIdQueryHandler>();
        services.AddScoped<IHandler<WorkoutDto, UpdateWorkoutByIdCommand>, UpdateWorkoutByIdCommandHandler>();
        return services;
    }
}