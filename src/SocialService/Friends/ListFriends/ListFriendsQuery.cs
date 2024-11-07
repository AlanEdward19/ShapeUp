namespace SocialService.Friends.ListFriends;

/// <summary>
/// Query para listar amigos de um perfil.
/// </summary>
public class ListFriendsQuery
{
    /// <summary>
    /// Id do perfil.
    /// </summary>
    public Guid ProfileId { get; private set; }
    
    /// <summary>
    /// Página atual.
    /// </summary>
    public int Page { get; private set; } = 1;
    
    /// <summary>
    /// Quantidade de registros por página.
    /// </summary>
    public int Rows { get; private set; } = 100;
    
    /// <summary>
    /// Método para setar o Id do perfil.
    /// </summary>
    /// <param name="profileId"></param>
    public void SetProfileId(Guid profileId)
    {
        ProfileId = profileId;
    }
    
    /// <summary>
    /// Método para setar a página atual.
    /// </summary>
    /// <param name="page"></param>
    public void SetPage(int page)
    {
        Page = page;
    }
    
    /// <summary>
    /// Método para setar a quantidade de registros por página.
    /// </summary>
    /// <param name="rows"></param>
    public void SetRows(int rows)
    {
        Rows = rows;
    }
}