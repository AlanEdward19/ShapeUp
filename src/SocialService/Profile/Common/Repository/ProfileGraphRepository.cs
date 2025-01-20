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
        var query = $@"
        CREATE (p:Profile {{
            id: '{profile.Id}',
            email: '{profile.Email}',
            firstName: '{profile.FirstName}',
            lastName: '{profile.LastName}',
            country: '{profile.Country}',
            city: '{profile.City}',
            state: '{profile.State}',
            imageUrl: '{profile.ImageUrl}',
            createdAt: datetime('{profile.CreatedAt:yyyy-MM-ddTHH:mm:ss}'),
            updatedAt: datetime('{profile.UpdatedAt:yyyy-MM-ddTHH:mm:ss}'),
            gender: '{profile.Gender}',
            birthDate: date('{profile.BirthDate:yyyy-MM-dd}'),
            bio: '{profile.Bio}'
        }})";

        await graphContext.ExecuteQueryAsync(query);
    }

    /// <summary>
    ///     Método para deletar um perfil
    /// </summary>
    /// <param name="id"></param>
    public async Task DeleteProfileAsync(Guid id)
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
    /// <returns></returns>
    public async Task<Profile> GetProfileAsync(Guid id)
    {
        var query = $@"
    MATCH (profile:Profile {{id: '{id}'}})
    OPTIONAL MATCH (profile)<-[:FOLLOWING]-(follower:Profile)
    OPTIONAL MATCH (profile)-[:FOLLOWING]->(following:Profile)
    OPTIONAL MATCH (profile)-[:PUBLISHED_BY]->(post:Post)
    RETURN profile, COUNT(follower) AS followers, COUNT(following) AS following, COUNT(post) AS posts";

        var result = await graphContext.ExecuteQueryAsync(query);

        var record = result.FirstOrDefault();

        if (record == null)
            return null;

        Profile profile = new Profile();
        var parsedDictionary = record["profile"].As<INode>().Properties.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        parsedDictionary["followers"] = record["followers"].As<int>();
        parsedDictionary["following"] = record["following"].As<int>();
        parsedDictionary["posts"] = record["posts"].As<int>();

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
        var query = $@"
    MATCH (p:Profile {{id: '{profile.Id}'}})
    SET p.email = '{profile.Email}',
        p.firstName = '{profile.FirstName}',
        p.lastName = '{profile.LastName}',
        p.country = '{profile.Country}',
        p.city = '{profile.City}',
        p.state = '{profile.State}',
        p.imageUrl = '{profile.ImageUrl}',
        p.updatedAt = datetime('{profile.UpdatedAt:yyyy-MM-ddTHH:mm:ss}'),
        p.gender = '{profile.Gender}',
        p.birthDate = date('{profile.BirthDate:yyyy-MM-dd}'),
        p.bio = '{profile.Bio}'
    RETURN p";

        await graphContext.ExecuteQueryAsync(query);
    }

    /// <summary>
    ///     Método para obter perfis
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Profile>> GetProfilesAsync(IEnumerable<Guid> ids)
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
            var parsedDictionary = record["profile"].As<INode>().Properties.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            parsedDictionary["followers"] = record["followers"].As<int>();
            parsedDictionary["following"] = record["following"].As<int>();
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
    public async Task<bool> ProfileExistsAsync(Guid id)
    {
        var query = $@"
    MATCH (p:Profile {{id: '{id}'}})
    RETURN p";

        var result = await graphContext.ExecuteQueryAsync(query);

        return result.Any();
    }
}