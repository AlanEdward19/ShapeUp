using ChatService.Chat.Common;
using ChatService.Chat.Common.Repository;
using ChatService.Chat.Common.Service;
using ChatService.Chat.GetMessages;
using ChatService.Chat.GetRecentMessages;
using ChatService.Chat.SendMessage;
using ChatService.Common.Interfaces;
using SharedKernel.Utils;

namespace ChatService.Chat;

/// <summary>
///     Modulo para resolver as dependências relacionadas a chats
/// </summary>
public static class ChatModule
{
    /// <summary>
    ///     Método para resolver as dependências relacionadas a chats
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureChatRelatedDependencies(this IServiceCollection services)
    {
        services
            .AddRepositories()
            .AddHandlers()
            .AddPublishers();

        return services;
    }

    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<IHandler<IEnumerable<ChatMessage>, GetRecentMessagesQuery>, GetRecentMessagesQueryHandler>();
        services.AddScoped<IHandler<IEnumerable<ChatMessage>, GetMessagesQuery>, GetMessagesQueryHandler>();
        services.AddScoped<IHandler<bool, SendMessageCommand>, SendMessageCommandHandler>();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IChatMongoRepository, ChatMongoRepository>();

        return services;
    }
    
    private static IServiceCollection AddPublishers(this IServiceCollection services)
    {
        services.AddScoped<INotificationPublisher, NotificationPublisher>();

        return services;
    }
}