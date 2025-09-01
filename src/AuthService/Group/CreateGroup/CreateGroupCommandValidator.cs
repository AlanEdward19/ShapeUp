using FluentValidation;

namespace AuthService.Group.CreateGroup;

public class CreateGroupCommandValidator : AbstractValidator<CreateGroupCommand>
{
    public CreateGroupCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Group name must not be empty.")
            .MaximumLength(100).WithMessage("Group name must not exceed 100 characters.");
        
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Group description must not be empty.")
            .MaximumLength(255).WithMessage("Group description must not exceed 255 characters.");
    }
}