using NotificationService.Common.Interfaces;
using NotificationService.Notification.Common.Service;
using NotificationService.Notification.SendNotification;

namespace NotificationService.Notification;

/// <summary>
///     Modulo para resolver as dependências relacionadas a notificações
/// </summary>
public static class NotificationModule
{
    /// <summary>
    ///     Método para resolver as dependências relacionadas a notificações
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureNotificationRelatedDependencies(this IServiceCollection services)
    {
        services
            .AddServices()
            .AddHandlers();

        return services;
    }

    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddTransient<IHandler<bool, SendNotificationCommand>, SendNotificationCommandHandler>();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<INotificationService, Common.Service.NotificationService>();
        
        services.AddSingleton<INotificationConsumer, NotificationConsumer>();
        services.AddSingleton<INotificationProcessor, NotificationProcessor>();

        return services;
    }
}