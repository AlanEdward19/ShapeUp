using Neo4j.Driver;

namespace SocialService.Database.Graph;

public class GraphContext(IDriver neo4jDriver)
{
    public async Task<IEnumerable<IRecord>?> ExecuteQueryAsync(string query)
    {
        var session = neo4jDriver.AsyncSession();
        var results = new List<IRecord>();
        try
        {
            var response = await session.RunAsync(query);

            while (await response.FetchAsync())
            {
                results.Add(response.Current);
            }
            
        }
        finally
        {
            await session.CloseAsync();
        }
        
        return results;
    }
    
    public async Task CreateNodeIfDoesntExists(string node)
    {
        var query = $"MATCH (r:{node}) RETURN COUNT(r) > 0 AS nodeExists";
        var response = ((await ExecuteQueryAsync(query))!).First();
        bool exists =  response["nodeExists"].As<bool>();

        if (!exists)
        {
            query = $"CREATE (n:{node})";
            await ExecuteQueryAsync(query);
        }
    }
}