using FluentValidation;
using SocialService.Profile.Common.Repository;

namespace SocialService.Profile.ViewProfile;

/// <summary>
/// Validador para a query de visualização de perfil
/// </summary>
public class ViewProfileQueryValidator : AbstractValidator<ViewProfileQuery>
{
    /// <summary>
    /// Validações para a query de visualização de perfil
    /// </summary>
    /// <param name="repository"></param>
    public ViewProfileQueryValidator(IProfileGraphRepository repository)
    {
        RuleFor(x => x.ProfileId)
            .MustAsync(async (id, cancellationToken) => await repository.ProfileExistsAsync(id))
            .WithMessage("ProfileId: '{PropertyValue}' doesn't exist.");
    }
}