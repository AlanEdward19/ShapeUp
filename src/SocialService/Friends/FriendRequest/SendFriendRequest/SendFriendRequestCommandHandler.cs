using SharedKernel.Dtos;
using SharedKernel.Enums;
using SharedKernel.Providers;
using SharedKernel.Utils;
using SocialService.Friends.Common.Repository;
using SocialService.Profile.Common.Repository;

namespace SocialService.Friends.FriendRequest.SendFriendRequest;

/// <summary>
///     Handler para adicionar um amigo.
/// </summary>
/// <param name="graphRepository"></param>
public class SendFriendRequestCommandHandler(
    IFriendshipGraphRepository graphRepository,
    IProfileGraphRepository profileGraphRepository,
    IGrpcProvider grpcProvider)
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

        NotificationDto notificationDto = new()
        {
            RecipientId = requestCommand.FriendId,
            Title = "Novo pedido de amizade",
            Topic = ENotificationTopic.FriendRequest,
            Body = $"{profile.FirstName} {profile.LastName} enviou uma solicitação de amizade.",
            Metadata = new()
            {
                { "userId", requestCommand.FriendId.ToString() }
            }
        };

        await grpcProvider.SendNotification(notificationDto, cancellationToken);

        return true;
    }
}