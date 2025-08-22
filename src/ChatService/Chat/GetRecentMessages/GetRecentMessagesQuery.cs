namespace ChatService.Chat.GetRecentMessages;

/// <summary>
/// Query para obter as mensagens recentes
/// </summary>
public class GetRecentMessagesQuery
{
    /// <summary>
    /// Id do perfil que deseja obter as mensagens
    /// </summary>
    public string ProfileId { get; private set; }
    
    /// <summary>
    /// Se a mensagem é de um chat de um profissional
    /// </summary>
    public bool IsProfessionalChat { get; set; }
    
    /// <summary>
    /// Página que deseja obter as mensagens
    /// </summary>
    public int Page { get; private set; }
    
    /// <summary>
    /// Método para definir o Id do perfil que deseja obter as mensagens
    /// </summary>
    /// <param name="profileId"></param>
    public void SetProfileId(string profileId) => ProfileId = profileId;
    
    /// <summary>
    /// Método para definir a página que deseja obter as mensagens
    /// </summary>
    /// <param name="page"></param>
    public void SetPage(int page) => Page = page;
    
    /// <summary>
    /// Método para definir se a mensagem é de um chat de um profissional
    /// </summary>
    /// <param name="isProfessionalChat"></param>
    public void SetIsProfessionalChat(bool isProfessionalChat) => IsProfessionalChat = isProfessionalChat;
}