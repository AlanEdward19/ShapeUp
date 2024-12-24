namespace SocialService.Recommendation.GetFriendRecommendations;

/// <summary>
///     Query para obter recomendações de amigos
/// </summary>
public class GetFriendRecommendationQuery
{
    /// <summary>
    ///     Página
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    ///     Quantidade de registros
    /// </summary>
    public int Rows { get; set; } = 10;
}