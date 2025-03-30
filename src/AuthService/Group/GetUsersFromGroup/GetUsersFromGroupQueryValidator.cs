using AuthService.Common;
using AuthService.Connections.Database;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Group.GetUsersFromGroup;

public class GetUsersFromGroupQueryValidator : AbstractValidator<GetUsersFromGroupQuery>
{
    public GetUsersFromGroupQueryValidator(AuthDbContext dbContext)
    {
        RuleFor(x => x)
            .CustomAsync(async (query, context, cancellationToken) =>
            {
                var group = await dbContext.Groups
                    .Include(x => x.Users)
                    .FirstOrDefaultAsync(x => x.Id == query.GroupId, cancellationToken);

                if (group == null)
                    context.AddFailure("Group", $"Group with id {query.GroupId} not found");

                Guid profileId = ProfileContext.ProfileId;

                if (group!.Users.Select(x => x.UserId).Contains(profileId))
                    context.AddFailure("Group", "You are not a member of this group");
            });
    }
}