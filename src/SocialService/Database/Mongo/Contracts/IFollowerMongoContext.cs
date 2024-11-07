using SocialService.Follow;

namespace SocialService.Database.Mongo.Contracts;

public interface IFollowerMongoContext
{
    #region Profile

    Task AddProfileDocumentAsync(ProfileConnections document);
    Task<ProfileConnections> GetProfileDocumentByIdAsync(Guid profileId);
    Task DeleteProfileDocumentByIdAsync(Guid objectId);

    #endregion

    #region Follower

    Task FollowProfileAsync(Guid userId, Guid followId);
    Task UnfollowProfileAsync(Guid userId, Guid followId);

    #endregion
}