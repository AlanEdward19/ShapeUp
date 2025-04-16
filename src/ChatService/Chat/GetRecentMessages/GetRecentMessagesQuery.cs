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
}