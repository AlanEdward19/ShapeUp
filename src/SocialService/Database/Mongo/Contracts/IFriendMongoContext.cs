using SocialService.Friends;

namespace SocialService.Database.Mongo.Contracts;

/// <summary>
/// Interface para o contexto de banco de dados relacionados a amigos.
/// </summary>
public interface IFriendMongoContext
{
    #region Profile

    /// <summary>
    /// Método para adicionar um documento de perfil.
    /// </summary>
    /// <param name="document"></param>
    /// <returns></returns>
    Task AddProfileDocumentAsync(Friend document);

    /// <summary>
    /// Método para obter um documento de perfil por Id.
    /// </summary>
    /// <param name="profileId"></param>
    /// <returns></returns>
    Task<Friend> GetProfileDocumentByIdAsync(Guid profileId);

    /// <summary>
    /// Método para deletar um documento de perfil por Id.
    /// </summary>
    /// <param name="objectId"></param>
    /// <returns></returns>
    Task DeleteProfileDocumentByIdAsync(Guid objectId);

    /// <summary>
    /// Método para obter a lista de amigos de um perfil.
    /// </summary>
    /// <param name="objectId"></param>
    /// <returns></returns>
    Task<IEnumerable<Friendship>> GetProfileFriendListByIdAsync(Guid objectId);

    #endregion

    #region Friend

    /// <summary>
    /// Método para adicionar um amigo a um perfil.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="friend"></param>
    /// <returns></returns>
    Task AddFriendAsync(Guid userId, Friendship friend);

    /// <summary>
    /// Método para remover um amigo de um perfil.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="friendId"></param>
    /// <returns></returns>
    Task RemoveFriendAsync(Guid userId, Guid friendId);

    #endregion

    #region Friend Request

    /// <summary>
    /// Método para adicionar um convite de amizade.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="invite"></param>
    /// <returns></returns>
    Task AddFriendshipInviteAsync(Guid userId, FriendshipInvite invite);

    /// <summary>
    /// Método para aceitar um convite de amizade.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="friendId"></param>
    /// <returns></returns>
    Task AcceptFriendshipInviteAsync(Guid userId, Guid friendId);

    /// <summary>
    /// Método para recusar um convite de amizade.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="friendId"></param>
    /// <returns></returns>
    Task DeclineFriendshipInviteAsync(Guid userId, Guid friendId);

    /// <summary>
    /// Método para remover um convite de amizade.
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="removeFromSent"></param>
    /// <returns></returns>
    Task RemoveRequestFromProfile(Guid from, Guid to, bool removeFromSent);

    #endregion
}