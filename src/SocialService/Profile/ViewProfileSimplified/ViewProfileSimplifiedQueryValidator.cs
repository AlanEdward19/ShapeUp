using SocialService.Profile.Common.Repository;

namespace SocialService.Profile.ViewProfileSimplified;

/// <summary>
///     Validador para a query de visualização de perfil
/// </summary>
public class ViewProfileSimplifiedQueryValidator : AbstractValidator<ViewProfileSimplifiedQuery>
{
    /// <summary>
    ///     Validações para a query de visualização de perfil
    /// </summary>
    /// <param name="repository"></param>
    public ViewProfileSimplifiedQueryValidator(IProfileGraphRepository repository)
    {
        RuleFor(x => x.ProfileId)
            .MustAsync(async (id, _) => await repository.ProfileExistsAsync(id))
            .WithMessage("ProfileId: '{PropertyValue}' doesn't exist.");
    }
}