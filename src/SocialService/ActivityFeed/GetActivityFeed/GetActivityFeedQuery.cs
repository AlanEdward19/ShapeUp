namespace SocialService.ActivityFeed.GetActivityFeed;

/// <summary>
///     Query para obter o feed de atividades.
/// </summary>
public class GetActivityFeedQuery
{
    /// <summary>
    ///     Página.
    /// </summary>
    public int Page { get; private set; } = 1;

    /// <summary>
    ///     Quantidade de registros por página.
    /// </summary>
    public int Rows { get; private set; } = 100;
}