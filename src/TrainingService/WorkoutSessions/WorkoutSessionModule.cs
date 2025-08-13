using TrainingService.Common.Interfaces;
using TrainingService.WorkoutSessions.Common.Repository;
using TrainingService.WorkoutSessions.CreateWorkoutSession;
using TrainingService.WorkoutSessions.DeleteWorkoutSessionById;
using TrainingService.WorkoutSessions.GetWorkoutSessionById;
using TrainingService.WorkoutSessions.GetWorkoutSessionByUserId;
using TrainingService.WorkoutSessions.UpdateWorkoutSessionById;

namespace TrainingService.WorkoutSessions;

/// <summary>
/// Modulo para resolver as dependências relacionadas a sessões de treino
/// </summary>
public static class WorkoutSessionModule
{
    /// <summary>
    /// Método para resolver as dependências relacionadas a sessões de treino
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureWorkoutSessionRelatedDependencies(this IServiceCollection services)
    {
        services
            .AddRepositories()
            .AddHandlers();
        return services;
    }

    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<IHandler<WorkoutSession, CreateWorkoutSessionCommand>, CreateWorkoutSessionCommandHandler>();
        services.AddScoped<IHandler<bool, DeleteWorkoutSessionByIdCommand>, DeleteWorkoutSessionByIdCommandHandler>();
        services.AddScoped<IHandler<WorkoutSession, UpdateWorkoutSessionByIdCommand>, UpdateWorkoutSessionByIdCommandHandler>();
        services.AddScoped<IHandler<WorkoutSession, GetWorkoutSessionByIdQuery>, GetWorkoutSessionByIdQueryHandler>();
        services.AddScoped<IHandler<ICollection<WorkoutSession>, GetWorkoutSessionsByUserIdQuery>, GetWorkoutSessionsByUserIdQueryHandler>();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IWorkoutSessionMongoRepository, WorkoutSessionMongoRepository>();

        return services;
    }
}