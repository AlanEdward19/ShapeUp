using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;

namespace SocialService.Connections.Search;

/// <summary>
/// Provedor de busca do Azure Search.
/// </summary>
public class AzureSearchProvider : IAzureSearchProvider
{
    private readonly ISearchIndexClient _indexClient;
    private const string IndexName = "profiles";

    /// <summary>
    /// Construtor da classe AzureSearchProvider
    /// </summary>
    /// <param name="configuration"></param>
    public AzureSearchProvider(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Search");
        
        var searchServiceClient = CreateSearchServiceClient(connectionString!);
        _indexClient = searchServiceClient.Indexes.GetClient(IndexName);
    }

    private SearchServiceClient CreateSearchServiceClient(string connectionString)
    {
        var connectionParts = connectionString.Split(';');
        
        string endpoint = connectionParts
            .FirstOrDefault(part => part.StartsWith("Endpoint=", StringComparison.OrdinalIgnoreCase))?
            .Substring("Endpoint=".Length)
            .Trim()!;

        string apiKey = connectionParts
            .FirstOrDefault(part => part.StartsWith("Key=", StringComparison.OrdinalIgnoreCase))?
            .Substring("Key=".Length)
            .Trim()!;
        
        var searchCredentials = new SearchCredentials(apiKey);
        
        return new SearchServiceClient(endpoint, searchCredentials);
    }
    
    /// <summary>
    /// Método para inserir ou atualizar um perfil no índice de busca.
    /// </summary>
    /// <param name="profileId"></param>
    /// <param name="name"></param>
    public async Task UpsertAsync(string profileId, string name)
    {
        var batch = IndexBatch.MergeOrUpload(new[]
        {
            new Document
            {
                { "ProfileId", profileId },
                { "Name", name }
            }
        });

        await _indexClient.Documents.IndexAsync(batch);
    }
    
    /// <summary>
    /// Método para deletar um perfil do índice de busca.
    /// </summary>
    /// <param name="profileId"></param>
    public async Task DeleteAsync(string profileId)
    {
        var batch = IndexBatch.Delete("ProfileId", new[] { profileId });

        await _indexClient.Documents.IndexAsync(batch);
    }

    /// <summary>
    /// Método para fazer uma pesquisa parcial com fuzzy search (busca por nome com margem de erro de até 2 caracteres)
    /// </summary>
    /// <param name="searchTerm"></param>
    /// <returns></returns>
    public async Task<IEnumerable<AzureSearchProfileDto>> SearchAsync(string searchTerm)
    {
        var parameters = new SearchParameters
        {
            Select = new[] { "ProfileId", "Name" },
            Top = 10,
            Filter = null
        };

        // Busca fuzzy com até 2 caracteres de erro
        var query = $"{searchTerm}~2"; // '2' especifica que até 2 caracteres podem ser diferentes

        var results = await _indexClient.Documents.SearchAsync(query, parameters);

        return results.Results
            .Select(r => new AzureSearchProfileDto(r.Document["ProfileId"].ToString(),  r.Document["Name"].ToString()))
            .ToList();
    }
}