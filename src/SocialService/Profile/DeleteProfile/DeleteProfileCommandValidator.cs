using FluentValidation;
using SocialService.Profile.Common.Repository;

namespace SocialService.Profile.DeleteProfile;

/// <summary>
/// Validador para o comando de deletar um perfil.
/// </summary>
public class DeleteProfileCommandValidator : AbstractValidator<DeleteProfileCommand>
{
    /// <summary>
    /// Validações para o comando de deletar um perfil.
    /// </summary>
    /// <param name="repository"></param>
    public DeleteProfileCommandValidator(IProfileGraphRepository repository)
    {
        RuleFor(x => x.ProfileId)
            .NotEmpty()
            .WithMessage("ProfileId cannot be empty.");
        
        RuleFor(x => x.ProfileId)
            .MustAsync(async (id, cancellationToken) => await repository.ProfileExistsAsync(id))
            .WithMessage("ProfileId: '{PropertyValue}' doesn't exist.");
    }
}