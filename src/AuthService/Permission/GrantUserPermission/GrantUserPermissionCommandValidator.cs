using AuthService.Connections.Database;
using FluentValidation;

namespace AuthService.Permission.GrantUserPermission;

public class GrantUserPermissionCommandValidator : AbstractValidator<GrantUserPermissionCommand>
{
    public GrantUserPermissionCommandValidator(AuthDbContext dbContext)
    {
        RuleFor(x => x.UserId)
            .CustomAsync(async (userId, context, cancellationToken) =>
            {
                if (await dbContext.Users.FindAsync(userId, cancellationToken) == null)
                    context.AddFailure("User", $"User with id {userId} not found");
            });

        RuleFor(x => x.PermissionId)
            .CustomAsync(async (permissionId, context, cancellationToken) =>
            {
                if (await dbContext.Permissions.FindAsync(permissionId, cancellationToken) == null)
                    context.AddFailure("Permission", $"Permission with id {permissionId} not found");
            });
    }
}