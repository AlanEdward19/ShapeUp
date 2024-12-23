using FluentValidation;
using SocialService.Profile.Common.Repository;

namespace SocialService.Profile.GetProfilePictures;

/// <summary>
/// Validador para a query de obter fotos de perfil.
/// </summary>
public class GetProfilePicturesQueryValidator : AbstractValidator<GetProfilePicturesQuery>
{
    /// <summary>
    /// Validações para a query de obter fotos de perfil.
    /// </summary>
    public GetProfilePicturesQueryValidator(IProfileGraphRepository repository)
    {
        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Page must be greater than or equal to 1.");
        
        RuleFor(x => x.Rows)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Rows must be greater than or equal to 1.");
        
        RuleFor(x => x.Rows)
            .LessThanOrEqualTo(100)
            .WithMessage("Rows must be less than or equal to 100.");
        
        RuleFor(x => x.ProfileId)
            .MustAsync(async (id, cancellationToken) => await repository.ProfileExistsAsync(id))
            .WithMessage("ProfileId: '{PropertyValue}' doesn't exist.");
    }
}