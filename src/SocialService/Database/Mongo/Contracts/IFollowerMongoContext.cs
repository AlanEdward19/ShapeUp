using SocialService.Follow;

namespace SocialService.Database.Mongo.Contracts;

/// <summary>
/// Interface para contexto de banco de dados relacionado a seguidores.
/// </summary>
public interface IFollowerMongoContext
{
    #region Profile

    /// <summary>
    /// Método para adicionar um documento de perfil.
    /// </summary>
    /// <param name="document"></param>
    /// <returns></returns>
    Task AddProfileDocumentAsync(ProfileConnections document);
    
    /// <summary>
    /// Método para obter um documento de perfil por Id.
    /// </summary>
    /// <param name="profileId"></param>
    /// <returns></returns>
    Task<ProfileConnections> GetProfileDocumentByIdAsync(Guid profileId);
    
    /// <summary>
    /// Método para deletar um documento de perfil por Id.
    /// </summary>
    /// <param name="objectId"></param>
    /// <returns></returns>
    Task DeleteProfileDocumentByIdAsync(Guid objectId);

    #endregion

    #region Follower

    /// <summary>
    /// Método para seguir um perfil.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="followId"></param>
    /// <returns></returns>
    Task FollowProfileAsync(Guid userId, Guid followId);
    
    /// <summary>
    /// Método para deixar de seguir um perfil.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="followId"></param>
    /// <returns></returns>
    Task UnfollowProfileAsync(Guid userId, Guid followId);

    #endregion
}