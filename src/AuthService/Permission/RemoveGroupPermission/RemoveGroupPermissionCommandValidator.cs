using AuthService.Connections.Database;
using AuthService.Permission.RemoveUserPermission;
using FluentValidation;

namespace AuthService.Permission.RemoveGroupPermission;

public class RemoveGroupPermissionCommandValidator : AbstractValidator<RemoveGroupPermissionCommand>
{
    public RemoveGroupPermissionCommandValidator(AuthDbContext dbContext)
    {
        RuleFor(x => x.GroupId)
            .CustomAsync(async (groupId, context, cancellationToken) =>
            {
                if (await dbContext.Groups.FindAsync(groupId, cancellationToken) == null)
                    context.AddFailure("Group", $"Group with id {groupId} not found");
            });

        RuleFor(x => x.PermissionId)
            .CustomAsync(async (permissionId, context, cancellationToken) =>
            {
                if (await dbContext.Permissions.FindAsync(permissionId, cancellationToken) == null)
                    context.AddFailure("Permission", $"Permission with id {permissionId} not found");
            });
    }
}