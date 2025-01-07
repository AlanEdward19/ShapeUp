namespace SocialService.Post.GetPostsByProfileId;

/// <summary>
/// Query para obter os posts de um perfil
/// </summary>
public class GetPostsByProfileIdQuery
{
    /// <summary>
    /// Id do perfil
    /// </summary>
    public Guid ProfileId { get; private set; }
    
    /// <summary>
    /// Número da página
    /// </summary>
    public int Page { get; private set; }
    
    /// <summary>
    /// Quantidade de linhas
    /// </summary>
    public int Rows { get; private set; }

    /// <summary>
    /// Construtor da query de busca de post
    /// </summary>
    /// <param name="page"></param>
    /// <param name="rows"></param>
    public GetPostsByProfileIdQuery(int page, int rows)
    {
        Page = page;
        Rows = rows;
    }

    /// <summary>
    /// Método para setar o id do perfil
    /// </summary>
    /// <param name="profileId"></param>
    public void SetProfileId(Guid profileId) => ProfileId = profileId;
}