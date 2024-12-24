using FluentValidation;
using SocialService.Profile.Common.Repository;

namespace SocialService.Friends.Friendship.ListFriends;

/// <summary>
/// Validador para a query de listar amigos de um perfil.
/// </summary>
public class ListFriendsQueryValidator : AbstractValidator<ListFriendsQuery>
{
    /// <summary>
    /// Validações para a query de listar amigos de um perfil.
    /// </summary>
    /// <param name="repository"></param>
    public ListFriendsQueryValidator(IProfileGraphRepository repository)
    {
        RuleFor(x => x.ProfileId)
            .MustAsync(async (profileId, cancellationToken) => await repository.ProfileExistsAsync(profileId))
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