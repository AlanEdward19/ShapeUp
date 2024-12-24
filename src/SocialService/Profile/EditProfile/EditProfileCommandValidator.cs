namespace SocialService.Profile.EditProfile;

/// <summary>
///     Validador para o comando de editar um perfil
/// </summary>
public class EditProfileCommandValidator : AbstractValidator<EditProfileCommand>
{
    /// <summary>
    ///     Validação para o comando de editar um perfil
    /// </summary>
    public EditProfileCommandValidator()
    {
        RuleFor(x => x.Gender)
            .IsInEnum()
            .WithMessage("Gender: '{PropertyValue}' isn't a valid value")
            .When(x => x.Gender is not null);

        RuleFor(x => x.BirthDate)
            .NotNull()
            .WithMessage("BirthDate is required")
            .When(x => x.BirthDate is not null);

        RuleFor(x => x.Bio)
            .MaximumLength(500)
            .WithMessage("Bio must have a maximum of 500 characters")
            .When(x => x.Bio is not null);
    }
}