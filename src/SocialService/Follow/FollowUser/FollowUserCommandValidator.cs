using SocialService.Profile.Common.Repository;

namespace SocialService.Follow.FollowUser;

/// <summary>
///     Validador para o comando de seguir um usuário.
/// </summary>
public class FollowUserCommandValidator : AbstractValidator<FollowUserCommand>
{
    /// <summary>
    ///     Validações para o comando de seguir um usuário.
    /// </summary>
    /// <param name="repository"></param>
    public FollowUserCommandValidator(IProfileGraphRepository repository)
    {
        RuleFor(x => x.FollowedUserId)
            .MustAsync(async (followedUserId, cancellationToken) => await repository.ProfileExistsAsync(followedUserId))
            .WithMessage("FollowedUserId: '{PropertyValue}' doesn't exist.");
    }
}