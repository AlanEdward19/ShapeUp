using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;

namespace SocialService.Connections.Search;

public class AzureSearchProvider : IAzureSearchProvider
{
    private readonly ISearchIndexClient _indexClient;
    private const string IndexName = "profiles";

    public AzureSearchProvider(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Search");
        
        var searchServiceClient = CreateSearchServiceClient(connectionString);
        _indexClient = searchServiceClient.Indexes.GetClient(IndexName);
    }

    private SearchServiceClient CreateSearchServiceClient(string connectionString)
    {
        var connectionParts = connectionString.Split(';');
        string endpoint = connectionParts.FirstOrDefault(part => part.StartsWith("Endpoint="))?.Substring("Endpoint=".Length);
        string apiKey = connectionParts.FirstOrDefault(part => part.StartsWith("ApiKey="))?.Substring("Key=".Length);
        
        var searchCredentials = new SearchCredentials(apiKey);
        
        return new SearchServiceClient(endpoint, searchCredentials);
    }
    
    public async Task UpsertAsync(Guid profileId, string name)
    {
        var batch = IndexBatch.MergeOrUpload(new[]
        {
            new Document
            {
                { "ProfileId", profileId.ToString() },
                { "Name", name }
            }
        });

        await _indexClient.Documents.IndexAsync(batch);
    }
    
    public async Task DeleteAsync(Guid profileId)
    {
        var batch = IndexBatch.Delete("ProfileId", new[] { profileId.ToString() });

        await _indexClient.Documents.IndexAsync(batch);
    }

    // Método para fazer uma pesquisa parcial com fuzzy search (busca por nome com margem de erro de até 2 caracteres)
    public async Task<IEnumerable<Profile>> SearchAsync(string searchTerm)
    {
        var parameters = new SearchParameters
        {
            Select = new[] { "ProfileId", "Name" }, // Campos que serão retornados
            Top = 10, // Limite de resultados
            Filter = null
        };

        // Busca fuzzy com até 2 caracteres de erro
        var query = $"{searchTerm}~2"; // '2' especifica que até 2 caracteres podem ser diferentes

        var results = await _indexClient.Documents.SearchAsync(query, parameters);

        return results.Results.Select(r => new Profile
        {
            ProfileId = Guid.Parse(r.Document["ProfileId"].ToString()),
            Name = r.Document["Name"].ToString()
        }).ToList();
    }
}

public class Profile
{
    public Guid ProfileId { get; set; }
    public string Name { get; set; }
}
