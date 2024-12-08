namespace SocialService.Profile.ViewProfile;

/// <summary>
///     Query para visualização de perfil.
/// </summary>
public class ViewProfileQuery(Guid profileId)
{
    /// <summary>
    ///     Id do perfil.
    /// </summary>
    public Guid ProfileId { get; private set; } = profileId;
}