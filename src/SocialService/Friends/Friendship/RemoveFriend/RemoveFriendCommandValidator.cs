using SocialService.Profile.Common.Repository;

namespace SocialService.Friends.Friendship.RemoveFriend;

/// <summary>
///     Validador para o comando de remover um amigo.
/// </summary>
public class RemoveFriendCommandValidator : AbstractValidator<RemoveFriendCommand>
{
    /// <summary>
    ///     Validações para o comando de remover um amigo.
    /// </summary>
    /// <param name="repository"></param>
    public RemoveFriendCommandValidator(IProfileGraphRepository repository)
    {
        RuleFor(x => x.ProfileId)
            .MustAsync(async (profileId, cancellationToken) => await repository.ProfileExistsAsync(profileId))
            .WithMessage("ProfileId: '{PropertyValue}' doesn't exist.");
    }
}