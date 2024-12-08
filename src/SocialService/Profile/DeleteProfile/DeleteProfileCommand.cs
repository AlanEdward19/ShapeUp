namespace SocialService.Profile.DeleteProfile;

/// <summary>
///     Comando para deletar um perfil.
/// </summary>
public class DeleteProfileCommand(Guid profileId)
{
    /// <summary>
    ///     Identificador do perfil.
    /// </summary>
    public Guid ProfileId { get; private set; } = profileId;
}