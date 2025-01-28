using SocialService.Common.Enums;
using SocialService.Common.Events;
using SocialService.Common.Services;
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
    INotificationPublisher notificationPublisher) : IHandler<bool, ReactToPostCommand>
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
        Guid postOwnerId = await postGraphRepository.GetProfileIdByPostIdAsync(command.PostId);
        
        Profile.Profile profile = await profileGraphRepository.GetProfileAsync(ProfileContext.ProfileId);
        
        await repository.ReactToPostAsync(reaction);

        if(postOwnerId != ProfileContext.ProfileId)
        {
            NotificationEvent @event = new()
            {
                RecipientId = postOwnerId,
                Topic = ENotificationTopic.Reaction,
                Content = $"{profile.FirstName} {profile.LastName} reagiu ao seu post: {command.PostId}",
            };

            await notificationPublisher.PublishNotificationEventAsync(@event);
        }

        return true;
    }
}