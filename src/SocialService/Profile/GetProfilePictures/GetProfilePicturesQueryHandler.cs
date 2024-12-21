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
    /// <param name="item"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<IEnumerable<ProfilePicture>> HandleAsync(GetProfilePicturesQuery item, CancellationToken cancellationToken)
    {
        return storageProvider.GetProfilePicturesAsync(item.ProfileId, item.Page, item.Rows);
    }
}