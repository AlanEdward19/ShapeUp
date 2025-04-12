namespace SocialService.Profile.DeleteProfile;

/// <summary>
///     Comando para deletar um perfil.
/// </summary>
public class DeleteProfileCommand(string profileId)
{
    /// <summary>
    ///     Identificador do perfil.
    /// </summary>
    public string ProfileId { get; private set; } = profileId;
}