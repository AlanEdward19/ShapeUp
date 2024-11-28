using Neo4j.Driver;
using SocialService.Database.Graph;

namespace SocialService.Friends.Common.Repository;

/// <summary>
/// Repositório de grafo sobre amizades.
/// </summary>
/// <param name="graphContext"></param>
public class FriendshipGraphRepository(
    GraphContext graphContext) : IFriendshipGraphRepository
{
    /// <summary>
    /// Método para enviar um pedido de amizade
    /// </summary>
    /// <param name="senderProfileId"></param>
    /// <param name="receiverProfileId"></param>
    /// <param name="message"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task SendFriendRequestAsync(Guid senderProfileId, Guid receiverProfileId, string message)
    {
        var existingRequest = await GetFriendRequestAsync(senderProfileId, receiverProfileId);

        if (existingRequest != null)
            throw new InvalidOperationException("There is already a pending request between these profiles.");

        var query = $@"""
    MATCH (sender:Profile {{id: '{senderProfileId}'}}), (receiver:Profile {{id: '{receiverProfileId}'}})
    CREATE (sender)-[:FRIEND_REQUEST {{
        id: '{Guid.NewGuid()}',
        message: '{message}',
        createdAt: datetime()
    }}]->(receiver)
            """;

        await graphContext.ExecuteQueryAsync(query);
    }

    /// <summary>
    /// Método para obter um pedido de amizade
    /// </summary>
    /// <param name="senderProfileId"></param>
    /// <param name="receiverProfileId"></param>
    /// <returns></returns>
    public async Task<FriendRequest?> GetFriendRequestAsync(Guid senderProfileId, Guid receiverProfileId)
    {
        var query = $@"""
    MATCH (sender:Profile {{id: '{senderProfileId}'}})-[r:FRIEND_REQUEST]->(receiver:Profile {{id: '{receiverProfileId}'}})
    RETURN r
        """;

        var responseList = (await graphContext.ExecuteQueryAsync(query));

        if (responseList == null || !responseList.Any())
            return null;

        var response = responseList.First().First().Value;

        if (response == null)
            return null;

        FriendRequest result = new();
        var parsedResponse = response.As<IRelationship>().Properties.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

        parsedResponse.Add("senderProfileId", senderProfileId);
        parsedResponse.Add("receiverProfileId", receiverProfileId);

        result.MapToEntityFromNeo4j(parsedResponse);

        return result;
    }

    /// <summary>
    /// Método para aceitar um pedido de amizade
    /// </summary>
    /// <param name="senderProfileId"></param>
    /// <param name="receiverProfileId"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task AcceptFriendRequestAsync(Guid senderProfileId, Guid receiverProfileId)
    {
        var request = await GetFriendRequestAsync(senderProfileId, receiverProfileId);

        if (request == null)
            throw new InvalidOperationException("The friend request does not exist or has already been accepted.");

        #region Create Friendship

        var query = $@"""
MATCH (profileA:Profile {{id: '{senderProfileId}'}}), (profileB:Profile {{id: '{receiverProfileId}'}})
CREATE (profileA)-[:FRIEND {{
    createdAt: datetime()
}}]->(profileB)
""";
        await graphContext.ExecuteQueryAsync(query);

        #endregion

        #region Delete Friend Request

        query = $@"
MATCH (sender:Profile {{id: '{senderProfileId}'}})-[r:FRIEND_REQUEST]->(receiver:Profile {{id: '{receiverProfileId}'}})
DELETE r";

        await graphContext.ExecuteQueryAsync(query);

        #endregion
    }

    /// <summary>
    /// Método para rejeitar um pedido de amizade
    /// </summary>
    /// <param name="senderProfileId"></param>
    /// <param name="receiverProfileId"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task RejectFriendRequestAsync(Guid senderProfileId, Guid receiverProfileId)
    {
        var request = await GetFriendRequestAsync(senderProfileId, receiverProfileId);
        if (request == null)
            throw new InvalidOperationException("The friend request does not exist or has already been rejected.");

        #region Delete Friend Request

        string query = $@"
MATCH (sender:Profile {{id: '{senderProfileId}'}})-[r:FRIEND_REQUEST]->(receiver:Profile {{id: '{receiverProfileId}'}})
DELETE r";

        await graphContext.ExecuteQueryAsync(query);

        #endregion
    }

    /// <summary>
    /// Método para obter os pedidos de amizade pendentes para um perfil
    /// </summary>
    /// <param name="receiverProfileId"></param>
    /// <returns></returns>
    public async Task<IEnumerable<FriendRequest>> GetPendingRequestsForProfileAsync(Guid receiverProfileId)
    {
        var query = $@"
    MATCH (sender:Profile)-[r:FRIEND_REQUEST]->(receiver:Profile {{id: '{receiverProfileId}'}})
    RETURN r, sender.id AS senderProfileId";

        var result = await graphContext.ExecuteQueryAsync(query);
        var requests = new List<FriendRequest>();

        foreach (var record in result)
        {
            var relationship = record["r"].As<IRelationship>();
            var friendRequest = new FriendRequest();

            var parsedResponse = relationship.Properties.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            parsedResponse.Add("senderProfileId", record["senderProfileId"].ToString()!);
            parsedResponse.Add("receiverProfileId", receiverProfileId.ToString());

            friendRequest.MapToEntityFromNeo4j(parsedResponse);
            requests.Add(friendRequest);
        }

        return requests;
    }

    /// <summary>
    /// Método para obter os pedidos de amizade enviados por um perfil
    /// </summary>
    /// <param name="senderProfileId"></param>
    /// <returns></returns>
    public async Task<List<FriendRequest>> GetSentFriendRequestsAsync(Guid senderProfileId)
    {
        var query = $@"
    MATCH (sender:Profile {{id: '{senderProfileId}'}})-[r:FRIEND_REQUEST]->(receiver:Profile)
    RETURN r, receiver.id AS receiverProfileId";

        var result = await graphContext.ExecuteQueryAsync(query);
        var requests = new List<FriendRequest>();

        foreach (var record in result)
        {
            var relationship = record["r"].As<IRelationship>();
            var friendRequest = new FriendRequest();

            var parsedResponse = relationship.Properties.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            parsedResponse.Add("senderProfileId", senderProfileId.ToString());
            parsedResponse.Add("receiverProfileId", record["receiverProfileId"].ToString()!);

            friendRequest.MapToEntityFromNeo4j(parsedResponse);
            requests.Add(friendRequest);
        }

        return requests;
    }

    /// <summary>
    /// Método para obter as amizades de um perfil
    /// </summary>
    /// <param name="profileId"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Friendship>> GetFriendshipsForProfileAsync(Guid profileId)
    {
        var query = $@"
    MATCH (profile:Profile {{id: '{profileId}'}})-[:FRIEND]-(friend:Profile)
    RETURN friend.id AS id";
        
        var result = await graphContext.ExecuteQueryAsync(query);
        var friendships = new List<Friendship>();

        foreach (var item in result)
            friendships.Add(new(profileId, Guid.Parse(item["id"].ToString()!)));

        return friendships;
    }

    /// <summary>
    /// Método para desfazer uma amizade
    /// </summary>
    /// <param name="profileAId"></param>
    /// <param name="profileBId"></param>
    public async Task UnfriendAsync(Guid profileAId, Guid profileBId)
    {
        var query = $@"
    MATCH (a:Profile {{id: '{profileAId}'}})-[r:FRIEND]-(b:Profile {{id: '{profileBId}'}})
    DELETE r";

        await graphContext.ExecuteQueryAsync(query);
    }
}