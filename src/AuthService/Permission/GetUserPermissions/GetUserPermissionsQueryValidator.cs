using AuthService.Connections.Database;
using FluentValidation;

namespace AuthService.Permission.GetUserPermissions;

public class GetUserPermissionsQueryValidator : AbstractValidator<GetUserPermissionsQuery>
{
    public GetUserPermissionsQueryValidator(AuthDbContext dbContext)
    {
        RuleFor(x => x.UserId)
            .CustomAsync(async (userId, context, cancellationToken) =>
            {
                if (await dbContext.Users.FindAsync(userId, cancellationToken) == null)
                    context.AddFailure("User", $"User with id {userId} not found");
            });
    }
}