using SocialService.Common.Enums;
using SocialService.Common.Events;
using SocialService.Common.Services;
using SocialService.Post.Common.Repository;
using SocialService.Profile.Common.Repository;

namespace SocialService.Post.Comment.CommentOnPost;

/// <summary>
///     Handler para comentar em um post
/// </summary>
/// <param name="repository"></param>
public class CommentOnPostCommandHandler(IPostGraphRepository repository, IProfileGraphRepository profileGraphRepository, INotificationPublisher notificationPublisher)
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
        Guid profileId = await repository.GetProfileIdByPostIdAsync(command.PostId);
        Profile.Profile profile = await profileGraphRepository.GetProfileAsync(profileId);

        await repository.CommentOnPostAsync(comment);

        if (profileId != ProfileContext.ProfileId)
        {
            NotificationEvent @event = new()
            {
                RecipientId = profileId,
                Topic = ENotificationTopic.Comment,
                Content = $"{profile.FirstName} {profile.LastName} comentou em seu post: {command.PostId}",
            };

            await notificationPublisher.PublishNotificationEventAsync(@event);
        }

        return true;
    }
}