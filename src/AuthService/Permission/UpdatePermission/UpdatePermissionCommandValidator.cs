using AuthService.Connections.Database;
using FluentValidation;

namespace AuthService.Permission.UpdatePermission;

public class UpdatePermissionCommandValidator : AbstractValidator<UpdatePermissionCommand>
{
    public UpdatePermissionCommandValidator(AuthDbContext dbContext)
    {
        RuleFor(x => x.PermissionId)
            .CustomAsync(async (permissionId, context, cancellationToken) =>
            {
                if (await dbContext.Permissions.FindAsync(permissionId, cancellationToken) == null)
                    context.AddFailure("Permission", $"Permission with id {permissionId} not found");
            });
        
        RuleFor(x => x.Theme)
            .NotEmpty()
            .When(x => x.Theme != null)
            .WithMessage("Theme can't be empty");
        
        RuleFor(x => x.Action)
            .IsInEnum()
            .When(x => x.Action != null)
            .WithMessage("Action must be a valid value");
    }
}