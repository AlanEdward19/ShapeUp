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
    /// <param name="isProfessionalChat"></param>
    /// <returns></returns>
    Task<IEnumerable<ChatMessage>> GetRecentMessagesAsync(string userId, int page, bool isProfessionalChat);
    
    /// <summary>
    /// Método para obter mensagens entre dois perfis
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="otherUserId"></param>
    /// <param name="page"></param>
    /// <param name="isProfessionalChat"></param>
    /// <returns></returns>
    Task<IEnumerable<ChatMessage>> GetMessagesAsync(string userId, string otherUserId, int page, bool isProfessionalChat);
    
    /// <summary>
    /// Método para enviar mensagem
    /// </summary>
    /// <param name="senderId"></param>
    /// <param name="receiverId"></param>
    /// <param name="message"></param>
    /// <param name="isProfessionalChat"></param>
    /// <returns></returns>
    Task SendMessageAsync(string senderId, string receiverId, string message, bool isProfessionalChat);
}