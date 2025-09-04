using SharedKernel.Dtos;
using SharedKernel.Enums;
using SharedKernel.Providers;
using SharedKernel.Utils;
using SocialService.Post.Common.Repository;
using SocialService.Profile.Common.Repository;

namespace SocialService.Post.Comment.CommentOnPost;

/// <summary>
///     Handler para comentar em um post
/// </summary>
/// <param name="repository"></param>
public class CommentOnPostCommandHandler(
    IPostGraphRepository repository,
    IProfileGraphRepository profileGraphRepository,
    IGrpcProvider grpcProvider)
    : IHandler<bool, CommentOnPostCommand>
{
    /// <summary>
    ///     Método para comentar em um post
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> HandleAsync(CommentOnPostCommand command, CancellationToken cancellationToken)
    {
        Comment comment = new(command, ProfileContext.ProfileId);
        string profileId = await repository.GetProfileIdByPostIdAsync(command.PostId);
        Profile.Profile profile = await profileGraphRepository.GetProfileAsync(profileId);

        await repository.CommentOnPostAsync(comment);

        if (profileId != ProfileContext.ProfileId)
        {
            NotificationDto notificationDto = new()
            {
                RecipientId = profileId,
                Title = "Novo comentário em seu post",
                Topic = ENotificationTopic.Comment,
                Body = $"{profile.FirstName} {profile.LastName} comentou em seu post: {command.PostId}",
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