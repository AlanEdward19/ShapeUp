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
    Task SendFriendRequestAsync(Guid senderProfileId, Guid receiverProfileId, string message);

    /// <summary>
    ///     Método para obter uma solicitação de amizade.
    /// </summary>
    /// <param name="senderProfileId"></param>
    /// <param name="receiverProfileId"></param>
    /// <returns></returns>
    Task<FriendRequest?> GetFriendRequestAsync(Guid senderProfileId, Guid receiverProfileId);

    /// <summary>
    ///     Método para aceitar uma solicitação de amizade.
    /// </summary>
    /// <param name="senderProfileId"></param>
    /// <param name="receiverProfileId"></param>
    /// <returns></returns>
    Task AcceptFriendRequestAsync(Guid senderProfileId, Guid receiverProfileId);

    /// <summary>
    ///     Método para rejeitar uma solicitação de amizade.
    /// </summary>
    /// <param name="senderProfileId"></param>
    /// <param name="receiverProfileId"></param>
    /// <returns></returns>
    Task RejectFriendRequestAsync(Guid senderProfileId, Guid receiverProfileId);

    /// <summary>
    ///     Método para obter as solicitações pendentes de amizade.
    /// </summary>
    /// <param name="receiverProfileId"></param>
    /// <returns></returns>
    Task<IEnumerable<FriendRequest>> GetPendingRequestsForProfileAsync(Guid receiverProfileId);

    /// <summary>
    ///     Método para obter as solicitações de amizade enviadas.
    /// </summary>
    /// <param name="senderProfileId"></param>
    /// <returns></returns>
    Task<List<FriendRequest>> GetSentFriendRequestsAsync(Guid senderProfileId);

    /// <summary>
    ///     Método para obter as amizades de um perfil.
    /// </summary>
    /// <param name="profileId"></param>
    /// <returns></returns>
    Task<IEnumerable<Friendship>> GetFriendshipsForProfileAsync(Guid profileId);

    /// <summary>
    ///     Método para desfazer uma amizade.
    /// </summary>
    /// <param name="profileAId"></param>
    /// <param name="profileBId"></param>
    /// <returns></returns>
    Task UnfriendAsync(Guid profileAId, Guid profileBId);
}