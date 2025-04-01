namespace SocialService.Profile.ViewProfileSimplified;

/// <summary>
///     Query para visualização de perfil.
/// </summary>
public class ViewProfileSimplifiedQuery(string profileId)
{
    /// <summary>
    ///     Id do perfil.
    /// </summary>
    public string ProfileId { get; private set; } = profileId;
}