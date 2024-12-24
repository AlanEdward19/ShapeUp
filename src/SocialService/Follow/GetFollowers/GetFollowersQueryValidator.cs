using SocialService.Profile.Common.Repository;

namespace SocialService.Follow.GetFollowers;

/// <summary>
///     Validador para a query de obter os seguidores de um usuário.
/// </summary>
public class GetFollowersQueryValidator : AbstractValidator<GetFollowersQuery>
{
    /// <summary>
    ///     Validações para a query de obter os seguidores de um usuário.
    /// </summary>
    /// <param name="repository"></param>
    public GetFollowersQueryValidator(IProfileGraphRepository repository)
    {
        RuleFor(x => x.ProfileId)
            .MustAsync(async (followedUserId, cancellationToken) => await repository.ProfileExistsAsync(followedUserId))
            .WithMessage("ProfileId: '{PropertyValue}' doesn't exist.");

        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Page must be greater than or equal to 1.");

        RuleFor(x => x.Rows)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Rows must be greater than or equal to 1.");

        RuleFor(x => x.Rows)
            .LessThanOrEqualTo(1000)
            .WithMessage("Rows must be less than or equal to 1000.");
    }
}