using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using SocialService.Common.Interfaces;
using SocialService.Database.Mongo;
using SocialService.Friends;
using SocialService.Storage;
using DatabaseContext = SocialService.Database.Sql.DatabaseContext;

namespace SocialService.Profile.DeleteProfile;

/// <summary>
/// Handler para o comando de deletar um perfil
/// </summary>
/// <param name="context"></param>
/// <param name="storageProvider"></param>
/// <param name="mongoContext"></param>
public class DeleteProfileCommandHandler(
    DatabaseContext context,
    IStorageProvider storageProvider,
    IMongoContext mongoContext) :  IHandler<bool, DeleteProfileCommand>
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

        //Precisa definir forma disso ficar dentro da mesma transaction que o banco
        await mongoContext.DeleteProfileDocumentByIdAsync(profile.ObjectId);
        
        await context.SaveChangesAsync(cancellationToken);
        await context.Database.CommitTransactionAsync(cancellationToken);
        
        return true;
    }
}