using SharedKernel.Dtos;
using SharedKernel.Enums;
using SharedKernel.Providers;
using SharedKernel.Utils;
using SocialService.Follow.Common.Repository;
using SocialService.Profile.Common.Repository;

namespace SocialService.Follow.FollowUser;

/// <summary>
///     Handler para seguir um usuário.
/// </summary>
/// <param name="graphRepository"></param>
public class FollowUserCommandHandler(
    IFollowerGraphRepository graphRepository,
    IProfileGraphRepository profileGraphRepository,
    IGrpcProvider grpcProvider) : IHandler<bool, FollowUserCommand>
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

        NotificationDto notificationDto = new()
        {
            RecipientId = command.FollowedUserId,
            Title = "Novo seguidor",
            Topic = ENotificationTopic.NewFollower,
            Body = $"{profile.FirstName} {profile.LastName} começou a te seguir.",
            Metadata = new()
            {
                { "userId", profile.Id }
            }
        };

        await grpcProvider.SendNotification(notificationDto, cancellationToken);

        return true;
    }
}