using System.Security.Claims;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Providers.Grpc;

namespace SharedKernel.Filters;

public class TokenValidatorFilter: Attribute, IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var httpContext = context.HttpContext;
        var token = httpContext.Request.Headers["Authorization"].First()?.Split(" ")[1] ?? "";
        
        if(string.IsNullOrWhiteSpace(token))
            throw new UnauthorizedAccessException("Usuário não está autenticado");
        
        IGrpcProvider grpcProvider = httpContext.RequestServices.GetRequiredService<IGrpcProvider>();
        
        var user = await grpcProvider.VerifyToken(token, CancellationToken.None);
        
        if(!user.IsValid)
            throw new UnauthorizedAccessException("Token inválido");
        
        var identity = new ClaimsIdentity(user.Claims, "Firebase");
        var principal = new ClaimsPrincipal(identity);
        
        httpContext.User = principal;
    }
}