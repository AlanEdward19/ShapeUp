using AuthService.Common.User;
using AuthService.Common.User.Repository;
using AuthService.Protos;
using FirebaseAdmin.Auth;
using Grpc.Core;

namespace AuthService.Services;

public class AuthService(ILogger<AuthService> logger, IUserRepository userRepository) : Protos.AuthService.AuthServiceBase
{
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