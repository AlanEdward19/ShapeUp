using AuthService.Common;
using AuthService.Connections.Database;
using AuthService.Group.Common.Enums;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Utils;

namespace AuthService.Group.AddUserToGroup;

public class AddUserToGroupCommandValidator : AbstractValidator<AddUserToGroupCommand>
{
    public AddUserToGroupCommandValidator(AuthDbContext dbContext)
    {
        RuleFor(x => x)
            .CustomAsync(async (command, context, cancellationToken) =>
            {
                var group = await dbContext.Groups
                    .Include(x => x.Users)
                    .FirstOrDefaultAsync(x => x.Id == command.GroupId, cancellationToken);
                
                if (group == null)
                    context.AddFailure("Group", $"Group with id {command.GroupId} not found");

                string profileId = ProfileContext.ProfileId;
                
                if(!group!.Users.Select(x => x.UserId).Contains(profileId))
                    context.AddFailure("Group", "You are not a member of this group");

                EGroupRole userRole = group.Users.First(x => x.UserId == profileId).Role;
                
                if(userRole != EGroupRole.Admin && userRole != EGroupRole.Owner)
                    context.AddFailure("Group", "You are not an admin or owner of this group");
            });
    }
}