using ChatService.Chat.Common.Repository;
using ChatService.Chat.GetRecentMessages;
using ChatService.Chat.SendMessage;
using ChatService.Common.Interfaces;

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
            .AddHandlers();

        return services;
    }

    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<IHandler<IEnumerable<ChatMessage>, GetRecentMessagesQuery>, GetRecentMessagesQueryHandler>();
        services.AddScoped<IHandler<bool, SendMessageCommand>, SendMessageCommandHandler>();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IChatMongoRepository, ChatMongoRepository>();

        return services;
    }
}