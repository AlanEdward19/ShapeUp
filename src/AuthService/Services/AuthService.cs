using AuthService.Common.User;
using AuthService.Common.User.Repository;
using AuthService.Connections.Database;
using AuthService.Protos;
using FirebaseAdmin.Auth;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Enums;

namespace AuthService.Services;

public class AuthService(ILogger<AuthService> logger, AuthDbContext dbContext, IUserRepository userRepository) : Protos.AuthService.AuthServiceBase
{
    public override async Task<CheckPermissionResponse> CheckPermission(CheckPermissionRequest request,
        ServerCallContext context)
    {
        EPermissionAction parsedAction = (EPermissionAction)request.Action;
        string theme = request.Theme;
        string objectId = request.ObjectId;

        logger.LogInformation(
            "Checking permission for user with object id: '{ObjectId}'\nAction: '{Action}'\nTheme: '{Theme}'", objectId,
            parsedAction, theme);

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

    public override async Task<VerifyTokenResponse> VerifyToken(VerifyTokenRequest request, ServerCallContext context)
    {
        try
        {
            FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(request.Token);

            var userId = decodedToken.Uid;
            var email = decodedToken.Claims["email"].ToString();
            
            User? user = await userRepository.GetByObjectIdAsync(userId, context.CancellationToken);

            if (user == null)
            {
                user = new(userId, email!);
                
                await userRepository.AddAsync(user, context.CancellationToken);
            }
            
            var claims = decodedToken.Claims.Select(c => new Claim()
            {
                Type = c.Key,
                Value = c.Value.ToString()
            }).ToList();

            return new VerifyTokenResponse
            {
                Claims = { claims },
                IsValid = true
            };
        }
        catch (FirebaseAuthException ex) when (ex.AuthErrorCode == AuthErrorCode.ExpiredIdToken)
        {
            return new VerifyTokenResponse
            {
                Claims = { },
                IsValid = false
            };
        }
    }
}