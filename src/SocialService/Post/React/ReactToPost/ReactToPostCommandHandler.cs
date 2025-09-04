using SharedKernel.Dtos;
using SharedKernel.Enums;
using SharedKernel.Providers;
using SharedKernel.Utils;
using SocialService.Post.Common.Repository;
using SocialService.Profile.Common.Repository;

namespace SocialService.Post.React.ReactToPost;

/// <summary>
///     Handler para o comando de like em um post.
/// </summary>
/// <param name="repository"></param>
public class ReactToPostCommandHandler(
    IPostGraphRepository repository,
    IProfileGraphRepository profileGraphRepository,
    IPostGraphRepository postGraphRepository,
    IGrpcProvider grpcProvider) : IHandler<bool, ReactToPostCommand>
{
    /// <summary>
    ///     Método para lidar com o comando de like em um post.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> HandleAsync(ReactToPostCommand command, CancellationToken cancellationToken)
    {
        Reaction reaction = new(ProfileContext.ProfileId, command);
        string postOwnerId = await postGraphRepository.GetProfileIdByPostIdAsync(command.PostId);
        
        Profile.Profile profile = await profileGraphRepository.GetProfileAsync(ProfileContext.ProfileId);
        
        await repository.ReactToPostAsync(reaction);

        if(postOwnerId != ProfileContext.ProfileId)
        {
            NotificationDto notificationDto = new()
            {
                RecipientId =  postOwnerId,
                Title = "Nova reação em seu post",
                Topic =  ENotificationTopic.Reaction,
                Body = $"{profile.FirstName} {profile.LastName} reagiu ao seu post",
                Metadata = new()
                {
                    { "postId", command.PostId.ToString() }
                }
            };

            await grpcProvider.SendNotification(notificationDto, cancellationToken);
        }

        return true;
    }
}