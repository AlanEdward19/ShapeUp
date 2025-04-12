namespace SocialService.Friends.Common.Repository;

/// <summary>
///     Interface para o repositório de grafo de amizades.
/// </summary>
public interface IFriendshipGraphRepository
{
    /// <summary>
    ///     Método para enviar uma solicitação de amizade.
    /// </summary>
    /// <param name="senderProfileId"></param>
    /// <param name="receiverProfileId"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    Task SendFriendRequestAsync(string senderProfileId, string receiverProfileId, string message);

    /// <summary>
    ///     Método para verificar se um pedido de amizade existe
    /// </summary>
    /// <param name="senderProfileId"></param>
    /// <param name="receiverProfileId"></param>
    /// <returns></returns>
    Task<bool> FriendRequestExistsAsync(string senderProfileId, string receiverProfileId);

    /// <summary>
    ///     Método para aceitar uma solicitação de amizade.
    /// </summary>
    /// <param name="senderProfileId"></param>
    /// <param name="receiverProfileId"></param>
    /// <returns></returns>
    Task AcceptFriendRequestAsync(string senderProfileId, string receiverProfileId);

    /// <summary>
    ///     Método para rejeitar uma solicitação de amizade.
    /// </summary>
    /// <param name="senderProfileId"></param>
    /// <param name="receiverProfileId"></param>
    /// <returns></returns>
    Task RejectFriendRequestAsync(string senderProfileId, string receiverProfileId);

    /// <summary>
    ///     Método para obter as solicitações pendentes de amizade.
    /// </summary>
    /// <param name="receiverProfileId"></param>
    /// <returns></returns>
    Task<IEnumerable<FriendRequest.FriendRequest>> GetPendingRequestsForProfileAsync(string receiverProfileId);

    /// <summary>
    ///     Método para obter as solicitações de amizade enviadas.
    /// </summary>
    /// <param name="senderProfileId"></param>
    /// <returns></returns>
    Task<List<FriendRequest.FriendRequest>> GetSentFriendRequestsAsync(string senderProfileId);

    /// <summary>
    ///     Método para obter as amizades de um perfil.
    /// </summary>
    /// <param name="profileId"></param>
    /// <returns></returns>
    Task<IEnumerable<Friendship.Friendship>> GetFriendshipsForProfileAsync(string profileId);

    /// <summary>
    ///     Método para desfazer uma amizade.
    /// </summary>
    /// <param name="profileAId"></param>
    /// <param name="profileBId"></param>
    /// <returns></returns>
    Task UnfriendAsync(string profileAId, string profileBId);
}