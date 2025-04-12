using FluentValidation;

namespace AuthService.Permission.CreatePermission;

public class CreatePermissionCommandValidator : AbstractValidator<CreatePermissionCommand>
{
    public CreatePermissionCommandValidator()
    {
        RuleFor(x => x.Theme)
            .NotEmpty()
            .WithMessage("Theme can't be empty");
    }
}