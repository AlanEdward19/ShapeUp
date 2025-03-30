using AuthService.Connections.Database;
using FluentValidation;

namespace AuthService.Permission.GetGroupPermissions;

public class GetGroupPermissionsQueryValidator : AbstractValidator<GetGroupPermissionsQuery>
{
    public GetGroupPermissionsQueryValidator(AuthDbContext dbContext)
    {
        RuleFor(x => x.GroupId)
            .CustomAsync(async (groupId, context, cancellationToken) =>
            {
                if (await dbContext.Groups.FindAsync(groupId, cancellationToken) == null)
                    context.AddFailure("Group", $"Group with id {groupId} not found");
            });
    }
}