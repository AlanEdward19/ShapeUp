using SocialService.Connections.Search;
using SocialService.Profile.Common.Repository;

namespace SocialService.Profile.SearchProfileByName;

/// <summary>
/// Handler para a query de busca de perfil por nome.
/// </summary>
/// <param name="searchProvider"></param>
/// <param name="repository"></param>
/// <param name="blobStorageProvider"></param>
public class SearchProfileByNameQueryHandler(IAzureSearchProvider searchProvider, IProfileGraphRepository repository, IBlobStorageProvider blobStorageProvider) : IHandler<IEnumerable<ProfileSimplifiedDto>, SearchProfileByNameQuery>
{
    /// <summary>
    /// Método para lidar com a query de busca de perfil por nome.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<ProfileSimplifiedDto>> HandleAsync(SearchProfileByNameQuery query, CancellationToken cancellationToken)
    {
        IEnumerable<AzureSearchProfileDto> searchResults = await searchProvider.SearchAsync(query.Name);
        
        var profiles = await repository.GetProfilesAsync(searchResults.Select(x => x.ProfileId));
        
        List<ProfileSimplifiedDto> result = profiles.Select(x =>
        {
            ProfileSimplifiedDto simplifiedDto = new(x);

            if(!string.IsNullOrWhiteSpace(simplifiedDto.ImageUrl))
                simplifiedDto.SetImageUrl(blobStorageProvider.GenerateAuthenticatedUrl(simplifiedDto.ImageUrl, $"{x.Id}"));
            
            return simplifiedDto;
        }).ToList();
        
        return result;
    }
}