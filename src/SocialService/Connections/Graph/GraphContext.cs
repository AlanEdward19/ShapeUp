namespace SocialService.Connections.Graph;

/// <summary>
///     Classe de contexto para conexão com o banco de dados de grafo
/// </summary>
/// <param name="neo4JDriver"></param>
public class GraphContext(IDriver neo4JDriver)
{
    /// <summary>
    ///     Método para executar uma query no banco de dados de grafo
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public async Task<IEnumerable<IRecord>?> ExecuteQueryAsync(string query)
    {
        var session = neo4JDriver.AsyncSession();
        var results = new List<IRecord>();
        try
        {
            var response = await session.RunAsync(query);

            while (await response.FetchAsync()) results.Add(response.Current);
        }
        finally
        {
            await session.CloseAsync();
        }

        return results;
    }
}