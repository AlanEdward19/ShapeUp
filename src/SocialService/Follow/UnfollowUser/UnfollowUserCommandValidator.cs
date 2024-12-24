using SocialService.Profile.Common.Repository;

namespace SocialService.Follow.UnfollowUser;

/// <summary>
///     Validador para o comando de parar de seguir um usuário.
/// </summary>
public class UnfollowUserCommandValidator : AbstractValidator<UnfollowUserCommand>
{
    /// <summary>
    ///     Validações para o comando de parar de seguir um usuário.
    /// </summary>
    /// <param name="repository"></param>
    public UnfollowUserCommandValidator(IProfileGraphRepository repository)
    {
        RuleFor(x => x.UnfollowedUserId)
            .MustAsync(async (followedUserId, cancellationToken) => await repository.ProfileExistsAsync(followedUserId))
            .WithMessage("UnfollowedUserId: '{PropertyValue}' doesn't exist.");
    }
}