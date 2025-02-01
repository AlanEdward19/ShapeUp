using SocialService.Common.Enums;
using SocialService.Common.Events;
using SocialService.Common.Services;
using SocialService.Friends.Common.Repository;
using SocialService.Profile.Common.Repository;

namespace SocialService.Friends.FriendRequest.SendFriendRequest;

/// <summary>
///     Handler para adicionar um amigo.
/// </summary>
/// <param name="graphRepository"></param>
public class SendFriendRequestCommandHandler(IFriendshipGraphRepository graphRepository,  IProfileGraphRepository profileGraphRepository, INotificationPublisher notificationPublisher)
    : IHandler<bool, SendFriendRequestCommand>
{
    /// <summary>
    ///     Método para adicionar um amigo.
    /// </summary>
    /// <param name="requestCommand"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> HandleAsync(SendFriendRequestCommand requestCommand, CancellationToken cancellationToken)
    {
        Profile.Profile profile = await profileGraphRepository.GetProfileAsync(ProfileContext.ProfileId);
        await graphRepository.SendFriendRequestAsync(ProfileContext.ProfileId, requestCommand.FriendId,
            requestCommand.RequestMessage ?? "");
        
        NotificationEvent @event = new()
        {
            RecipientId = requestCommand.FriendId,
            Topic = ENotificationTopic.FriendRequest,
            Content = $"{profile.FirstName} {profile.LastName} enviou uma solicitação de amizade.",
        };

        await notificationPublisher.PublishNotificationEventAsync(@event);

        return true;
    }
}