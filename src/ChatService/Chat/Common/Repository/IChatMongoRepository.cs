namespace ChatService.Chat.Common.Repository;

/// <summary>
/// Interface para o repositório de chat
/// </summary>
public interface IChatMongoRepository
{
    /// <summary>
    /// Método para obter mensagens recentes
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="page"></param>
    /// <returns></returns>
    Task<IEnumerable<ChatMessage>> GetRecentMessagesAsync(string userId, int page);
    
    /// <summary>
    /// Método para obter mensagens entre dois perfis
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="otherUserId"></param>
    /// <param name="page"></param>
    /// <returns></returns>
    Task<IEnumerable<ChatMessage>> GetMessagesAsync(string userId, string otherUserId, int page);
    
    /// <summary>
    /// Método para enviar mensagem
    /// </summary>
    /// <param name="senderId"></param>
    /// <param name="receiverId"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    Task SendMessageAsync(Guid senderId, Guid receiverId, string message);
}