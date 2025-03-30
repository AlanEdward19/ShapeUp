using AuthService.Common;
using AuthService.Connections.Database;
using AuthService.Group.Common.Enums;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Group.RemoveUserFromGroup;

public class RemoveUserFromGroupCommandValidator : AbstractValidator<RemoveUserFromGroupCommand>
{
    public RemoveUserFromGroupCommandValidator(AuthDbContext dbContext)
    {
        RuleFor(x => x)
            .CustomAsync(async (command, context, cancellationToken) =>
            {
                var group = await dbContext.Groups
                    .Include(x => x.Users)
                    .FirstOrDefaultAsync(x => x.Id == command.GroupId, cancellationToken);
                
                if (group == null)
                    context.AddFailure("Group", $"Group with id {command.GroupId} not found");

                Guid profileId = ProfileContext.ProfileId;
                
                if(group!.Users.Select(x => x.UserId).Contains(profileId))
                    context.AddFailure("Group", "You are not a member of this group");

                EGroupRole userRole = group.Users.First(x => x.UserId == profileId).Role;
                
                if(userRole != EGroupRole.Admin && userRole != EGroupRole.Owner)
                    context.AddFailure("Group", "You are not an admin or owner of this group");
                
                if(group.Users.Select(x => x.UserId).Contains(command.UserId))
                    context.AddFailure("User", "User is not a member of this group");
            });
    }   
}