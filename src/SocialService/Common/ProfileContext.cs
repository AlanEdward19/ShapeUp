namespace SocialService.Common;

/// <summary>
/// Context para perfil logado
/// </summary>
public static class ProfileContext
{
    /// <summary>
    /// Id do perfil logado
    /// </summary>
    private static readonly AsyncLocal<Guid> _profileId = new();

    /// <summary>
    /// Método para obter e definir o Id do perfil logado
    /// </summary>
    public static Guid ProfileId
    {
        get => _profileId.Value;
        set => _profileId.Value = value;
    }
}