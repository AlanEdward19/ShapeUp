using SocialService.Common.Interfaces;
using SocialService.Connections.Storage;

namespace SocialService.Profile.GetProfilePictures;

/// <summary>
/// Handler para a query de obter fotos de perfil.
/// <param name="storageProvider"></param>
/// </summary>
public class GetProfilePicturesQueryHandler(IStorageProvider storageProvider) : IHandler<IEnumerable<ProfilePicture>, GetProfilePicturesQuery>
{
    /// <summary>
    /// Método para lidar com a query de obter fotos de perfil.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<IEnumerable<ProfilePicture>> HandleAsync(GetProfilePicturesQuery query, CancellationToken cancellationToken)
    {
        return storageProvider.GetProfilePicturesAsync(query.ProfileId, query.Page, query.Rows);
    }
}