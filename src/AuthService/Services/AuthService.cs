using AuthService.Common.User;
using AuthService.Connections.Database;
using AuthService.Protos;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Enums;

namespace AuthService.Services;

public class AuthService(ILogger<AuthService> logger, AuthDbContext dbContext) : Protos.AuthService.AuthServiceBase
{
    public override async Task<CheckPermissionResponse> CheckPermission(CheckPermissionRequest request, ServerCallContext context)
    {
        EPermissionAction parsedAction = (EPermissionAction)request.Action;
        string theme = request.Theme;
        Guid objectId = Guid.Parse(request.ObjectId);
     
        logger.LogInformation("Checking permission for user with object id: '{ObjectId}'\nAction: '{Action}'\nTheme: '{Theme}'", objectId, parsedAction, theme);
        
        User user = await dbContext.Users
            .AsNoTracking()
            .Include(x => x.UserGroups)
            .ThenInclude(x => x.Group)
            .ThenInclude(x => x.GroupPermissions)
            .ThenInclude(x => x.Permission)
            .FirstAsync(x => x.ObjectId == objectId);

        List<Permission.Permission> userPermissions = user.UserGroups
                .SelectMany(x => x.Group.GroupPermissions
                    .Select(gp => gp.Permission))
                .ToList();
        
        CheckPermissionResponse result = new()
        {
            IsAllowed = userPermissions.Any(x => x.Action == parsedAction && x.Theme == theme)
        };

        return result;
    }
}