using SocialService.Common.Interfaces;
using SocialService.Profile.Common.Repository;

namespace SocialService.Profile.ViewProfile;

/// <summary>
///     Handler para a query de visualização de perfil.
/// </summary>
/// <param name="context"></param>
public class ViewProfileQueryHandler(IProfileGraphRepository repository) : IHandler<ProfileAggregate, ViewProfileQuery>
{
    /// <summary>
    ///     Método para lidar com a query de visualização de perfil.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ProfileAggregate> HandleAsync(ViewProfileQuery query, CancellationToken cancellationToken)
    {
        Profile profile = await repository.GetProfileAsync(query.ProfileId);

        return new ProfileAggregate(profile);
    }
}