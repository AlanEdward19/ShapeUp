using SocialService.Friends;

namespace SocialService.Database.Mongo.Contracts;

public interface IFriendMongoContext
{
    #region Profile

    Task AddProfileDocumentAsync(Friend document);
    Task<Friend> GetProfileDocumentByIdAsync(Guid profileId);
    Task DeleteProfileDocumentByIdAsync(Guid objectId);
    Task<IEnumerable<Friendship>> GetProfileFriendListByIdAsync(Guid objectId);

    #endregion

    #region Friend

    Task AddFriendAsync(Guid userId, Friendship friend);
    Task RemoveFriendAsync(Guid userId, Guid friendId);

    #endregion

    #region Friend Request

    Task AddFriendshipInviteAsync(Guid userId, FriendshipInvite invite);
    Task AcceptFriendshipInviteAsync(Guid userId, Guid friendId);
    Task DeclineFriendshipInviteAsync(Guid userId, Guid friendId);
    Task RemoveRequestFromProfile(Guid from, Guid to, bool removeFromSent);

    #endregion
}