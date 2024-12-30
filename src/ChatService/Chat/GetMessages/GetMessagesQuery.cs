namespace ChatService.Chat.GetMessages;

/// <summary>
/// Query para obter as mensagens
/// </summary>
public class GetMessagesQuery
{
    /// <summary>
    /// Id do perfil A
    /// </summary>
    public Guid ProfileAId { get; private set; }
    
    /// <summary>
    /// Id do perfil B
    /// </summary>
    public Guid ProfileBId { get; private set; }
    
    /// <summary>
    /// Número da página
    /// </summary>
    public int Page { get; private set; }
    
    /// <summary>
    /// Método para definir o Id do perfil A
    /// </summary>
    /// <param name="profileId"></param>
    public void SetProfileAId(Guid profileId) => ProfileAId = profileId;
    
    /// <summary>
    /// Método para definir o Id do perfil B
    /// </summary>
    /// <param name="profileId"></param>
    public void SetProfileBId(Guid profileId) => ProfileBId = profileId;
    
    /// <summary>
    /// Método para definir a página
    /// </summary>
    /// <param name="page"></param>
    public void SetPage(int page) => Page = page;
}