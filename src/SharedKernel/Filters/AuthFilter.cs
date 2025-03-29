using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Enums;
using SharedKernel.Providers.Grpc;
using SharedKernel.Utils;

namespace SharedKernel.Filters;

public class AuthFilter(EPermissionAction action, string theme) : Attribute, IAsyncAuthorizationFilter
{
    private EPermissionAction Action { get; } = action;
    private string Theme { get; } = theme;
    

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var httpContext = context.HttpContext;

        IGrpcProvider grpcProvider = httpContext.RequestServices.GetRequiredService<IGrpcProvider>();
        
        Guid objectId = Guid.Parse(httpContext.User.GetObjectId());
        
        bool hasPermission = await grpcProvider.CheckUserPermission(objectId, Action, Theme, CancellationToken.None);
        
        if(!hasPermission)
            throw new UnauthorizedAccessException("Usuário não tem permissão para acessar este recurso");
        
    }
}