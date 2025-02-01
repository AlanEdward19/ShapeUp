namespace SocialService.Profile.CreateProfile;

/// <summary>
///     Validador para o comando de criação de perfil
/// </summary>
public class CreateProfileCommandValidator : AbstractValidator<CreateProfileCommand>
{
    /// <summary>
    ///     Validação para o comando de criação de perfil
    /// </summary>
    public CreateProfileCommandValidator()
    {
        RuleFor(x => x.Bio)
            .MaximumLength(500)
            .WithMessage("Bio must have a maximum of 500 characters")
            .When(x => x.Bio is not null);
    }
}