using Microsoft.EntityFrameworkCore;
using SocialService.Common.Interfaces;
using SocialService.Profile.Common.Repository;
using SocialService.Storage;
using DatabaseContext = SocialService.Database.Sql.DatabaseContext;

namespace SocialService.Profile.DeleteProfile;

/// <summary>
/// Handler para o comando de deletar um perfil
/// </summary>
/// <param name="context"></param>
/// <param name="storageProvider"></param>
/// <param name="friendMongoContext"></param>
public class DeleteProfileCommandHandler(
    DatabaseContext context,
    IStorageProvider storageProvider,
    IProfileGraphRepository graphRepository) :  IHandler<bool, DeleteProfileCommand>
{
    /// <summary>
    /// Método para lidar com o comando de deletar um perfil
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    public async Task<bool> HandleAsync(DeleteProfileCommand command, CancellationToken cancellationToken)
    {
        await context.Database.BeginTransactionAsync(cancellationToken);

        var profile = await context.Profiles.FirstAsync(x => x.ObjectId.Equals(command.ProfileId), cancellationToken);

        if (!string.IsNullOrWhiteSpace(profile.ImageUrl))
            await storageProvider.DeleteBlobAsync(profile.ImageUrl, "profile-pictures");

        context.Profiles.Remove(profile);
        await graphRepository.DeleteProfileAsync(command.ProfileId);
        
        await context.SaveChangesAsync(cancellationToken);
        await context.Database.CommitTransactionAsync(cancellationToken);
        
        return true;
    }
}