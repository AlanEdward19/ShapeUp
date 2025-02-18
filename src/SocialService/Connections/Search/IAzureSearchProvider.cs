namespace SocialService.Connections.Search;

public interface IAzureSearchProvider
{
    Task UpsertAsync(Guid profileId, string name);
    Task DeleteAsync(Guid profileId);
    Task<IEnumerable<AzureSearchProfileDto>> SearchAsync(string searchTerm);
}