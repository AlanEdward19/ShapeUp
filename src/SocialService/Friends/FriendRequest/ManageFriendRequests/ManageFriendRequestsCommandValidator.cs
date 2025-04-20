using SharedKernel.Utils;
using SocialService.Friends.Common.Repository;
using SocialService.Profile.Common.Repository;

namespace SocialService.Friends.FriendRequest.ManageFriendRequests;

/// <summary>
///     Validador para o comando de gerenciar solicitações de amizade.
/// </summary>
public class ManageFriendRequestsCommandValidator : AbstractValidator<ManageFriendRequestsCommand>
{
    /// <summary>
    ///     Validações para o comando de gerenciar solicitações de amizade.
    /// </summary>
    /// <param name="profileGraphRepository"></param>
    /// <param name="repository"></param>
    public ManageFriendRequestsCommandValidator(IProfileGraphRepository profileGraphRepository,
        IFriendshipGraphRepository repository)
    {
        RuleFor(x => x.ProfileId)
            .MustAsync(async (profileId, cancellationToken) =>
                await profileGraphRepository.ProfileExistsAsync(profileId))
            .WithMessage("ProfileId: '{PropertyValue}' doesn't exist.");

        RuleFor(x => x.ProfileId)
            .MustAsync(async (profileId, cancellationToken) =>
                await repository.FriendRequestExistsAsync(profileId, ProfileContext.ProfileId))
            .WithMessage("Friend request doesn't exist.");
    }
}