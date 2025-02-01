using SocialService.Profile;

namespace SocialService.Recommendation.GetFriendRecommendations;

/// <summary>
///     Classe de recomendação de amigos
///     <param name="profile"></param>
///     <param name="mutualFriends"></param>
/// </summary>
public class FriendRecommendation(ProfileSimplifiedDto profile, int mutualFriends)
{
    /// <summary>
    ///     Informações do perfil
    /// </summary>
    public ProfileSimplifiedDto Profile { get; private set; } = profile;

    /// <summary>
    ///     Quantidade de amigos em comum
    /// </summary>
    public int MutualFriends { get; private set; } = mutualFriends;
}