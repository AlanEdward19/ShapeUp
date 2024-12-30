namespace SocialService.Profile.ViewProfileSimplified;

/// <summary>
///     Query para visualização de perfil.
/// </summary>
public class ViewProfileSimplifiedQuery(Guid profileId)
{
    /// <summary>
    ///     Id do perfil.
    /// </summary>
    public Guid ProfileId { get; private set; } = profileId;
}