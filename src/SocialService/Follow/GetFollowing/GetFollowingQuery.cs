namespace SocialService.Follow.GetFollowing;

/// <summary>
/// Query para obter os perfis que um perfil está seguindo.
/// </summary>
public class GetFollowingQuery
{
    /// <summary>
    /// Id do perfil.
    /// </summary>
    public Guid ProfileId { get; private set; }
    
    /// <summary>
    /// Página.
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
    /// Método para setar a página.
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