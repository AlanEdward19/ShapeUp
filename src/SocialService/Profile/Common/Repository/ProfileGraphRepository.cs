namespace SocialService.Profile.Common.Repository;

/// <summary>
///     Repositório de grafo sobre perfis.
/// </summary>
/// <param name="graphContext"></param>
public class ProfileGraphRepository(GraphContext graphContext) : IProfileGraphRepository
{
    /// <summary>
    ///     Método para criar um perfil
    /// </summary>
    /// <param name="profile"></param>
    public async Task CreateProfileAsync(Profile profile)
    {
        var birthDateValue = profile.BirthDate.HasValue
            ? $"date('{profile.BirthDate:yyyy-MM-dd}')"
            : $"'{profile.BirthDate}'";
        var query = $@"
    CREATE (p:Profile {{
        id: '{profile.Id}',
        email: '{profile.Email}',
        firstName: '{profile.FirstName}',
        lastName: '{profile.LastName}',
        displayName: '{profile.DisplayName}',
        country: '{profile.Country}',
        postalCode: '{profile.PostalCode}',
        imageUrl: '{profile.ImageUrl}',
        createdAt: datetime('{profile.CreatedAt:yyyy-MM-ddTHH:mm:ss}'),
        updatedAt: datetime('{profile.UpdatedAt:yyyy-MM-ddTHH:mm:ss}'),
        gender: '{profile.Gender}',
        birthDate: {birthDateValue},
        bio: '{profile.Bio}',
        latitude: {profile.Latitude},
        longitude: {profile.Longitude}
    }})";

        await graphContext.ExecuteQueryAsync(query);
    }

    /// <summary>
    ///     Método para deletar um perfil
    /// </summary>
    /// <param name="id"></param>
    public async Task DeleteProfileAsync(string id)
    {
        var query = $@"
    MATCH (p:Profile {{id: '{id}'}})
    OPTIONAL MATCH (p)-[:PUBLISHED_BY]->(post:Post)
    OPTIONAL MATCH (p)-[:REACTED]->(post)
    OPTIONAL MATCH (p)-[:FRIEND]->(friend:Profile)
    OPTIONAL MATCH (p)-[:SENT_REQUEST]->(request:FriendRequest)
    OPTIONAL MATCH (p)<-[:RECEIVED_REQUEST]-(request)
    OPTIONAL MATCH (p)-[:FOLLOWING]->(followed:Profile)
    OPTIONAL MATCH (p)<-[:FOLLOWING]-(follower:Profile)
    OPTIONAL MATCH (post)<-[:COMMENTED_ON]-(comment:Comment)
    OPTIONAL MATCH (post)<-[r:REACTED]-()
    DETACH DELETE p, post, comment, r, friend, request, followed, follower";

        await graphContext.ExecuteQueryAsync(query);
    }

    /// <summary>
    ///     Método para obter um perfil
    /// </summary>
    /// <param name="id"></param>
    /// <param name="requesterId"></param>
    /// <returns></returns>
    public async Task<Profile?> GetProfileAsync(string id, string? requesterId = null)
    {
        var query = string.IsNullOrWhiteSpace(requesterId)
            ? $@"
    MATCH (profile:Profile {{id: '{id}'}})
    OPTIONAL MATCH (profile)<-[:FOLLOWING]-(follower:Profile)
    OPTIONAL MATCH (profile)-[:FOLLOWING]->(following:Profile)
    OPTIONAL MATCH (profile)-[:PUBLISHED_BY]->(post:Post)
    RETURN profile, SIZE(COLLECT(DISTINCT follower)) AS followers, SIZE(COLLECT(DISTINCT following)) AS following, COUNT(post) AS posts"
            : $@"
    MATCH (profile:Profile {{id: '{id}'}})
    OPTIONAL MATCH (profile)<-[:FOLLOWING]-(follower:Profile)
    OPTIONAL MATCH (profile)-[:FOLLOWING]->(following:Profile)
    OPTIONAL MATCH (profile)-[:PUBLISHED_BY]->(post:Post)
    OPTIONAL MATCH (profile)<-[:FRIEND]-(friend:Profile {{id: '{requesterId}'}})
    OPTIONAL MATCH (profile)<-[:FOLLOWING]-(followingRequester:Profile {{id: '{requesterId}'}})
    RETURN profile, 
           SIZE(COLLECT(DISTINCT follower)) AS followers, 
           SIZE(COLLECT(DISTINCT following)) AS following, 
           COUNT(post) AS posts,
           COUNT(friend) > 0 AS isFriend,
           COUNT(followingRequester) > 0 AS isFollowing";

        var result = await graphContext.ExecuteQueryAsync(query);

        var record = result.FirstOrDefault();

        if (record == null)
            return null;

        Profile profile = new Profile();
        var parsedDictionary = record["profile"].As<INode>().Properties.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        parsedDictionary["followers"] = record["followers"].As<int>();
        parsedDictionary["following"] = record["following"].As<int>();
        parsedDictionary["posts"] = record["posts"].As<int>();

        if (!string.IsNullOrWhiteSpace(requesterId))
        {
            parsedDictionary["isFriend"] = record["isFriend"].As<bool>();
            parsedDictionary["isFollowing"] = record["isFollowing"].As<bool>();
        }

        profile.MapToEntityFromNeo4j(parsedDictionary);

        return profile;
    }

    /// <summary>
    ///     Método para atualizar um perfil
    /// </summary>
    /// <param name="profile"></param>
    /// <exception cref="NotImplementedException"></exception>
    public async Task UpdateProfileAsync(Profile profile)
    {
        var birthDateValue = profile.BirthDate.HasValue
            ? $"date('{profile.BirthDate:yyyy-MM-dd}')"
            : $"'{profile.BirthDate}'";

        var query = $@"
    MATCH (p:Profile {{id: '{profile.Id}'}})
    SET p.email = '{profile.Email}',
        p.firstName = '{profile.FirstName}',
        p.lastName = '{profile.LastName}',
        p.postalCode = '{profile.PostalCode}',
        p.country = '{profile.Country}',
        p.displayName = '{profile.DisplayName}',
        p.imageUrl = '{profile.ImageUrl}',
        p.updatedAt = datetime('{profile.UpdatedAt:yyyy-MM-ddTHH:mm:ss}'),
        p.gender = '{profile.Gender}',
        p.birthDate = {birthDateValue},
        p.bio = '{profile.Bio}',
        p.latitude = {profile.Latitude},
        p.longitude = {profile.Longitude}
    RETURN p";

        await graphContext.ExecuteQueryAsync(query);
    }

    /// <summary>
    ///     Método para obter perfis
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Profile>> GetProfilesAsync(IEnumerable<string> ids)
    {
        var idsString = string.Join("','", ids);
        var query = $@"
    MATCH (profile:Profile)
    WHERE profile.id IN ['{idsString}']
    OPTIONAL MATCH (profile)<-[:FOLLOWING]-(follower:Profile)
    OPTIONAL MATCH (profile)-[:FOLLOWING]->(following:Profile)
    RETURN profile, COUNT(follower) AS followers, COUNT(following) AS following";

        var result = await graphContext.ExecuteQueryAsync(query);

        var profiles = result.Select(record =>
        {
            var profile = new Profile();
            var parsedDictionary =
                record["profile"].As<INode>().Properties.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            parsedDictionary["followers"] = record["followers"].As<int>();
            parsedDictionary["following"] = record["following"].As<int>();
            parsedDictionary["posts"] = 0;
            profile.MapToEntityFromNeo4j(parsedDictionary);
            return profile;
        }).ToList();

        return profiles;
    }

    /// <summary>
    ///     Método para verificar se um perfil existe
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<bool> ProfileExistsAsync(string id)
    {
        var query = $@"
    MATCH (p:Profile {{id: '{id}'}})
    RETURN p";

        var result = await graphContext.ExecuteQueryAsync(query);

        return result.Any();
    }
}