namespace SocialService.Connections.Search;

public interface IAzureSearchProvider
{
    Task UpsertAsync(string profileId, string name);
    Task DeleteAsync(string profileId);
    Task<IEnumerable<AzureSearchProfileDto>> SearchAsync(string searchTerm);
}