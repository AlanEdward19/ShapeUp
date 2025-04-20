using SharedKernel.Utils;
using SocialService.Friends.Common.Repository;
using SocialService.Profile.Common.Repository;

namespace SocialService.Friends.FriendRequest.RemoveFriendRequest;

/// <summary>
///     Validador para o comando de remover solicitação de amizade.
/// </summary>
public class RemoveFriendRequestCommandValidator : AbstractValidator<RemoveFriendRequestCommand>
{
    /// <summary>
    ///     Validações para o comando de remover solicitação de amizade..
    /// </summary>
    /// <param name="profileGraphRepository"></param>
    /// <param name="repository"></param>
    public RemoveFriendRequestCommandValidator(IProfileGraphRepository profileGraphRepository,
        IFriendshipGraphRepository repository)
    {
        RuleFor(x => x.ProfileId)
            .MustAsync(async (profileId, cancellationToken) =>
                await profileGraphRepository.ProfileExistsAsync(profileId))
            .WithMessage("ProfileId: '{PropertyValue}' doesn't exist.");

        RuleFor(x => x.ProfileId)
            .MustAsync(async (profileId, cancellationToken) =>
                await repository.FriendRequestExistsAsync(ProfileContext.ProfileId, profileId))
            .WithMessage("Friend request doesn't exist.");
    }
}