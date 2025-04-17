using SharedKernel.Utils;
using SocialService.Common.Enums;
using SocialService.Common.Events;
using SocialService.Common.Services;
using SocialService.Follow.Common.Repository;
using SocialService.Profile.Common.Repository;

namespace SocialService.Follow.FollowUser;

/// <summary>
///     Handler para seguir um usuário.
/// </summary>
/// <param name="graphRepository"></param>
public class FollowUserCommandHandler(IFollowerGraphRepository graphRepository, IProfileGraphRepository profileGraphRepository, INotificationPublisher notificationPublisher) : IHandler<bool, FollowUserCommand>
{
    /// <summary>
    ///     Método para seguir um usuário.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> HandleAsync(FollowUserCommand command, CancellationToken cancellationToken)
    {
        await graphRepository.FollowAsync(ProfileContext.ProfileId, command.FollowedUserId);
        Profile.Profile profile = await profileGraphRepository.GetProfileAsync(ProfileContext.ProfileId);
        
        NotificationEvent @event = new()
        {
            RecipientId = command.FollowedUserId,
            Topic = ENotificationTopic.NewFollower,
            Content = $"{profile.FirstName} {profile.LastName} começou a te seguir.",
        };

        await notificationPublisher.PublishNotificationEventAsync(@event);

        return true;
    }
}