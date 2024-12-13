namespace SocialService.Friends.FriendRequest.ManageFriendRequests;

/// <summary>
///     Comando para gerenciar solicitações de amizade.
/// </summary>
/// <param name="accept"></param>
/// <param name="profileId"></param>
public class ManageFriendRequestsCommand(bool accept, Guid profileId)
{
    /// <summary>
    ///     Aceitar ou recusar a solicitação.
    /// </summary>
    public bool Accept { get; private set; } = accept;

    /// <summary>
    ///     Id do perfil.
    /// </summary>
    public Guid ProfileId { get; private set; } = profileId;
}