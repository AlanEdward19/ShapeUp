namespace SharedKernel.Utils;

/// <summary>
///     Context para perfil logado
/// </summary>
public static class ProfileContext
{
    /// <summary>
    ///     Id do perfil logado
    /// </summary>
    private static readonly AsyncLocal<string> _profileId = new();

    /// <summary>
    ///     Método para obter e definir o Id do perfil logado
    /// </summary>
    public static string ProfileId
    {
        get => _profileId.Value;
        set => _profileId.Value = value;
    }
}