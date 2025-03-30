using AuthService.Connections.Database;
using FluentValidation;

namespace AuthService.Permission.DeletePermission;

public class DeletePermissionCommandValidator : AbstractValidator<DeletePermissionCommand>
{
    public DeletePermissionCommandValidator(AuthDbContext dbContext)
    {
        RuleFor(x => x.PermissionId)
            .CustomAsync(async (permissionId, context, cancellationToken) =>
            {
                if (await dbContext.Permissions.FindAsync(permissionId, cancellationToken) == null)
                    context.AddFailure("Permission", $"Permission with id {permissionId} not found");
            });
    }
}