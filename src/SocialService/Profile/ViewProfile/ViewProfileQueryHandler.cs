using Microsoft.EntityFrameworkCore;
using SocialService.Common.Interfaces;
using DatabaseContext = SocialService.Database.Sql.DatabaseContext;

namespace SocialService.Profile.ViewProfile;

/// <summary>
/// Handler para a query de visualização de perfil.
/// </summary>
/// <param name="context"></param>
public class ViewProfileQueryHandler(DatabaseContext context) : IHandler<ProfileAggregate, ViewProfileQuery>
{
    /// <summary>
    /// Método para lidar com a query de visualização de perfil.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ProfileAggregate> HandleAsync(ViewProfileQuery query, CancellationToken cancellationToken)
    {
        Profile profile = await context.Profiles.FirstAsync(
            x => x.ObjectId.Equals(query.ProfileId),
            cancellationToken);

        return new ProfileAggregate(profile);
    }
}