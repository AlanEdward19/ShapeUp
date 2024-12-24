using SocialService.Profile;

namespace SocialService.Recommendation.GetFriendRecommendations;

/// <summary>
///     Classe de recomendação de amigos
///     <param name="profile"></param>
///     <param name="mutualFriends"></param>
/// </summary>
public class FriendRecommendation(ProfileDto profile, int mutualFriends)
{
    /// <summary>
    ///     Informações do perfil
    /// </summary>
    public ProfileDto Profile { get; private set; } = profile;

    /// <summary>
    ///     Quantidade de amigos em comum
    /// </summary>
    public int MutualFriends { get; private set; } = mutualFriends;
}