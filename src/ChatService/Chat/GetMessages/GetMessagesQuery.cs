namespace ChatService.Chat.GetMessages;

/// <summary>
/// Query para obter as mensagens
/// </summary>
public class GetMessagesQuery
{
    /// <summary>
    /// Id do perfil A
    /// </summary>
    public string ProfileAId { get; private set; }
    
    /// <summary>
    /// Id do perfil B
    /// </summary>
    public string ProfileBId { get; private set; }
    
    /// <summary>
    /// Se a mensagem é de um chat de um profissional
    /// </summary>
    public bool IsProfessionalChat { get; set; }
    
    /// <summary>
    /// Número da página
    /// </summary>
    public int Page { get; private set; }
    
    /// <summary>
    /// Método para definir o Id do perfil A
    /// </summary>
    /// <param name="profileId"></param>
    public void SetProfileAId(string profileId) => ProfileAId = profileId;
    
    /// <summary>
    /// Método para definir o Id do perfil B
    /// </summary>
    /// <param name="profileId"></param>
    public void SetProfileBId(string profileId) => ProfileBId = profileId;
    
    /// <summary>
    /// Método para definir a página
    /// </summary>
    /// <param name="page"></param>
    public void SetPage(int page) => Page = page;
    
    /// <summary>
    /// Método para definir se a mensagem é de um chat de um profissional
    /// </summary>
    /// <param name="isProfessionalChat"></param>
    public void SetIsProfessionalChat(bool isProfessionalChat) => IsProfessionalChat = isProfessionalChat;
}