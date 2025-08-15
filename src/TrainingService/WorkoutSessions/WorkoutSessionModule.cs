using TrainingService.Common.Interfaces;
using TrainingService.WorkoutSessions.Common.Repository;
using TrainingService.WorkoutSessions.CreateWorkoutSession;
using TrainingService.WorkoutSessions.DeleteWorkoutSessionById;
using TrainingService.WorkoutSessions.GetCurrentWorkoutSessionByUserId;
using TrainingService.WorkoutSessions.GetWorkoutSessionById;
using TrainingService.WorkoutSessions.GetWorkoutSessionByUserId;
using TrainingService.WorkoutSessions.GetWorkoutSessionsByWorkoutId;
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
        services.AddScoped<IHandler<WorkoutSessionDto, CreateWorkoutSessionCommand>, CreateWorkoutSessionCommandHandler>();
        services.AddScoped<IHandler<bool, DeleteWorkoutSessionByIdCommand>, DeleteWorkoutSessionByIdCommandHandler>();
        services.AddScoped<IHandler<WorkoutSessionDto, UpdateWorkoutSessionByIdCommand>, UpdateWorkoutSessionByIdCommandHandler>();
        services.AddScoped<IHandler<WorkoutSessionDto, GetWorkoutSessionByIdQuery>, GetWorkoutSessionByIdQueryHandler>();
        services.AddScoped<IHandler<ICollection<WorkoutSessionDto>, GetWorkoutSessionsByUserIdQuery>, GetWorkoutSessionsByUserIdQueryHandler>();
        services.AddScoped<IHandler<WorkoutSessionDto, GetCurrentWorkoutSessionByUserIdQuery>, GetCurrentWorkoutSessionByUserIdQueryHandler>();
        services.AddScoped<IHandler<ICollection<WorkoutSessionDto>, GetWorkoutSessionsByWorkoutIdQuery>, GetWorkoutSessionsByWorkoutIdQueryHandler>();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IWorkoutSessionMongoRepository, WorkoutSessionMongoRepository>();

        return services;
    }
}