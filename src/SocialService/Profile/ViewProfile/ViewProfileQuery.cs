namespace SocialService.Profile.ViewProfile;

/// <summary>
///     Query para visualização de perfil.
/// </summary>
public class ViewProfileQuery(string profileId)
{
    /// <summary>
    ///     Id do perfil.
    /// </summary>
    public string ProfileId { get; private set; } = profileId;
}