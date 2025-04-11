using AuthService.Connections.Database;
using AuthService.Permission.GrantUserPermission;
using FluentValidation;

namespace AuthService.Permission.RemoveUserPermission;

public class RemoveUserPermissionCommandValidator : AbstractValidator<RemoveUserPermissionCommand>
{
    public RemoveUserPermissionCommandValidator(AuthDbContext dbContext)
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