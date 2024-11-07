using MongoDB.Driver;
using SocialService.Friends;
using Microsoft.Extensions.Logging;
using SocialService.Database.Mongo.Contracts;
using SocialService.Friends.ListFriends;

namespace SocialService.Database.Mongo;

/// <summary>
/// Contexto de banco de dados para amigos.
/// </summary>
public class FriendMongoContext : IFriendMongoContext
{
    private readonly IMongoCollection<Friend> _collection;
    private readonly ILogger<FriendMongoContext> _logger;

    /// <summary>
    /// Construtor
    /// </summary>
    /// <param name="connectionString"></param>
    /// <param name="databaseName"></param>
    /// <param name="collectionName"></param>
    /// <param name="logger"></param>
    public FriendMongoContext(string connectionString, string databaseName, string collectionName,
        ILogger<FriendMongoContext> logger)
    {
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _collection = database.GetCollection<Friend>(collectionName);
        _logger = logger;
    }

    #region Profile

    /// <summary>
    /// Método para adicionar um documento de perfil.
    /// </summary>
    /// <param name="document"></param>
    public async Task AddProfileDocumentAsync(Friend document)
    {
        try
        {
            await _collection.InsertOneAsync(document);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding profile document.");
            throw;
        }
    }

    /// <summary>
    /// Método para obter um documento de perfil por Id.
    /// </summary>
    /// <param name="profileId"></param>
    /// <returns></returns>
    public async Task<Friend> GetProfileDocumentByIdAsync(Guid profileId)
    {
        try
        {
            var filter = Builders<Friend>.Filter.Eq(x => x.ProfileId, profileId.ToString());
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting profile document by ID.");
            return default;
        }
    }

    /// <summary>
    /// Método para deletar um documento de perfil por Id.
    /// </summary>
    /// <param name="objectId"></param>
    public async Task DeleteProfileDocumentByIdAsync(Guid objectId)
    {
        try
        {
            var filter = Builders<Friend>.Filter.Eq(x => x.ProfileId, objectId.ToString());
            var result = await _collection.DeleteOneAsync(filter);
            _logger.LogInformation(result.DeletedCount > 0
                ? "Document deleted successfully."
                : "No document was deleted.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting profile document.");
        }
    }

    /// <summary>
    /// Método para obter a lista de amigos de um perfil.
    /// </summary>
    /// <param name="objectId"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Friendship>> GetProfileFriendListByIdAsync(Guid objectId)
    {
        try
        {
            var filter = Builders<Friend>.Filter.Eq(x => x.ProfileId, objectId.ToString());
            var result = await _collection.Find(filter).FirstOrDefaultAsync();

            return result.Friends;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting profile document by ID.");
            return default;
        }
    }

    #endregion

    #region Friend

    /// <summary>
    /// Método para adicionar um amigo a um perfil.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="friend"></param>
    public async Task AddFriendAsync(Guid userId, Friendship friend)
    {
        var filter = Builders<Friend>.Filter.Eq("profileId", userId.ToString());
        var userDocument = await _collection.Find(filter).FirstOrDefaultAsync();

        if (userDocument == null)
        {
            _logger.LogWarning("User not found.");
            return;
        }

        var insert = Builders<Friend>.Update.Push("friends", friend);

        await _collection.UpdateOneAsync(filter, insert);
        _logger.LogInformation("Friend added successfully.");
    }

    /// <summary>
    /// Método para remover um amigo de um perfil.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="friendId"></param>
    public async Task RemoveFriendAsync(Guid userId, Guid friendId)
    {
        var parsedFriendId = friendId.ToString();
        var filter = Builders<Friend>.Filter.Eq("profileId", userId.ToString());
        var userDocument = await _collection.Find(filter).FirstOrDefaultAsync();

        if (userDocument == null)
        {
            _logger.LogWarning("User not found.");
            return;
        }

        var friendExists = userDocument.Friends.Any(f => f.FriendId == parsedFriendId);
        if (friendExists)
        {
            // Temos que remover o amigo tanto do perfil do usuário quanto do perfil do amigo

            var updateFilter = Builders<Friend>.Filter.Eq("profileId", userId.ToString());
            var update = Builders<Friend>.Update.PullFilter("friends",
                Builders<Friendship>.Filter.Eq("friendId", parsedFriendId));
            await _collection.UpdateOneAsync(updateFilter, update);

            updateFilter = Builders<Friend>.Filter.Eq("profileId", parsedFriendId);
            update = Builders<Friend>.Update.PullFilter("friends",
                Builders<Friendship>.Filter.Eq("friendId", userId.ToString()));
            await _collection.UpdateOneAsync(updateFilter, update);

            _logger.LogInformation("Friend removed successfully.");
        }
        else
        {
            _logger.LogWarning("Friend not found.");
        }
    }

    #endregion

    #region Friend Request

    /// <summary>
    /// Método para adicionar um convite de amizade.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="invite"></param>
    public async Task AddFriendshipInviteAsync(Guid userId, FriendshipInvite invite)
    {
        var filter = Builders<Friend>.Filter.Eq("profileId", userId.ToString());
        var userDocument = await _collection.Find(filter).FirstOrDefaultAsync();

        if (userDocument == null)
        {
            _logger.LogWarning("User not found.");
            return;
        }

        var inviteExists = userDocument.InvitesSent.Any(i => i.FriendId == invite.FriendId);
        if (inviteExists)
        {
            _logger.LogWarning("Invite already exists.");
            return;
        }

        var friendFilter = Builders<Friend>.Filter.Eq("profileId", invite.FriendId);
        var friendDocument = await _collection.Find(friendFilter).FirstOrDefaultAsync();

        if (friendDocument == null)
        {
            _logger.LogWarning("Friend not found.");
            return;
        }

        var friendInviteExists = friendDocument.InvitesReceived.Any(i => i.FriendId == userId.ToString());
        if (friendInviteExists)
        {
            _logger.LogWarning("Friend invite already exists.");
            return;
        }

        var updateFilter = Builders<Friend>.Filter.Eq("profileId", invite.FriendId);
        var update = Builders<Friend>.Update.Push("invitesSent",
            new FriendshipInvite(userId.ToString(), invite.RequestMessage));
        await _collection.UpdateOneAsync(updateFilter, update);

        updateFilter = Builders<Friend>.Filter.Eq("profileId", userId.ToString());
        update = Builders<Friend>.Update.Push("invitesReceived", invite);
        await _collection.UpdateOneAsync(updateFilter, update);

        _logger.LogInformation("Invite added successfully.");
    }

    /// <summary>
    /// Método para aceitar um convite de amizade.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="friendId"></param>
    public async Task AcceptFriendshipInviteAsync(Guid userId, Guid friendId)
    {
        var parsedFriendId = friendId.ToString();
        var filter = Builders<Friend>.Filter.Eq("profileId", userId.ToString());
        var userDocument = await _collection.Find(filter).FirstOrDefaultAsync();

        if (userDocument == null)
        {
            _logger.LogWarning("User not found.");
            return;
        }

        var inviteExists = userDocument.InvitesReceived.Any(i => i.FriendId == parsedFriendId);
        if (inviteExists)
        {
            DateTime now = DateTime.Now;
            var friendship = new Friendship(parsedFriendId, now);
            await AddFriendAsync(userId, friendship);

            friendship = new Friendship(userId.ToString(), now);
            await AddFriendAsync(friendId, friendship);

            await RemoveRequestFromProfile(friendId, userId, true);
            await RemoveRequestFromProfile(userId, friendId, false);

            _logger.LogInformation("Invite accepted successfully.");
        }
        else
        {
            _logger.LogWarning("Invite not found.");
        }
    }

    /// <summary>
    /// Método para recusar um convite de amizade.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="friendId"></param>
    public async Task DeclineFriendshipInviteAsync(Guid userId, Guid friendId)
    {
        var parsedFriendId = friendId.ToString();
        var filter = Builders<Friend>.Filter.Eq("profileId", userId.ToString());
        var userDocument = await _collection.Find(filter).FirstOrDefaultAsync();

        if (userDocument == null)
        {
            _logger.LogWarning("User not found.");
            return;
        }

        var inviteExists = userDocument.InvitesReceived.Any(i => i.FriendId == parsedFriendId);
        if (inviteExists)
        {
            await RemoveRequestFromProfile(friendId, userId, true);
            await RemoveRequestFromProfile(userId, friendId, false);
            _logger.LogInformation("Invite declined successfully.");
        }
        else
        {
            _logger.LogWarning("Invite not found.");
        }
    }

    /// <summary>
    /// Método para remover um convite de amizade.
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="removeFromSent"></param>
    public async Task RemoveRequestFromProfile(Guid from, Guid to, bool removeFromSent)
    {
        var filter = Builders<Friend>.Filter.Eq("profileId", from.ToString());
        var userDocument = await _collection.Find(filter).FirstOrDefaultAsync();

        if (userDocument == null)
        {
            _logger.LogWarning("User not found.");
            return;
        }

        var inviteExists = removeFromSent
            ? userDocument.InvitesSent.Any(i => i.FriendId == to.ToString())
            : userDocument.InvitesReceived.Any(i => i.FriendId == to.ToString());

        if (inviteExists)
        {
            var updateFilter = Builders<Friend>.Filter.And(
                Builders<Friend>.Filter.Eq("profileId", from.ToString()),
                Builders<Friend>.Filter.Eq(removeFromSent ? "invitesSent.friendId" : "invitesReceived.friendId",
                    to.ToString()));

            var update = Builders<Friend>.Update.PullFilter(removeFromSent ? "invitesSent" : "invitesReceived",
                Builders<FriendshipInvite>.Filter.Eq("friendId", to.ToString()));
            await _collection.UpdateOneAsync(updateFilter, update);
            _logger.LogInformation("Invite removed successfully.");
        }
        else
        {
            _logger.LogWarning("Invite not found.");
        }
    }

    #endregion
}