using SocialService.Database;
using DatabaseContext = SocialService.Database.Sql.DatabaseContext;

namespace SocialService.Profile.CreateProfile;

public class CreateProfileCommandHandler(DatabaseContext context)
{
    public async Task HandleAsync(CreateProfileCommand command, CancellationToken cancellationToken)
    {
        Profile profile = new(command);
        
        await context.Profiles.AddAsync(profile, cancellationToken);
        
        //Definir retorno
    }
}