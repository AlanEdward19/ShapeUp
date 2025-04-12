namespace SocialService.Profile.GetProfilePictures;

/// <summary>
///     Query para obter fotos de perfil.
/// </summary>
/// <param name="profileId"></param>
/// <param name="page"></param>
/// <param name="rows"></param>
public class GetProfilePicturesQuery(string profileId, int? page, int? rows)
{
    /// <summary>
    ///     Id do perfil.
    /// </summary>
    public string ProfileId { get; private set; } = profileId;

    /// <summary>
    ///     Página atual.
    /// </summary>
    public int Page { get; private set; } = page ?? 1;

    /// <summary>
    ///     Quantidade de registros por página.
    /// </summary>
    public int Rows { get; private set; } = rows ?? 10;
}