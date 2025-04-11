using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Filters;
using SharedKernel.Enums;

namespace SharedKernel.Filters;

public class AuthFilter(EPermissionAction action, string theme) : Attribute, IAsyncAuthorizationFilter
{
    private EPermissionAction Action { get; } = action;
    private string Theme { get; } = theme;

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var httpContext = context.HttpContext;
        string token = httpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var jwtHandler = new JwtSecurityTokenHandler();

        if (!jwtHandler.CanReadToken(token))
            throw new ArgumentException("Token inválido");

        var jwtToken = jwtHandler.ReadJwtToken(token);

        var exp = jwtToken.Payload.Exp;

        if (exp == null)
            throw new InvalidOperationException("O token não contém uma data de expiração");

        var expirationTime = DateTimeOffset.FromUnixTimeSeconds(exp.Value).UtcDateTime;

        if (DateTime.UtcNow >= expirationTime)
            throw new UnauthorizedAccessException("Token expirado");

        if (jwtToken.Payload.TryGetValue("scopes", out var scopesClaim) && scopesClaim is JsonElement claim && claim.ValueKind == JsonValueKind.Array)
        {
            List<string> scopesList = claim.EnumerateArray()
                .Select(element => element.GetString())
                .Where(value => value != null)
                .ToList()!;
            
            bool hasPermission = scopesList.Contains($"{Action} - {Theme}".ToLower());
        
            if(!hasPermission)
                throw new UnauthorizedAccessException("Usuário não tem permissão para acessar este recurso");

            return;
        }

        throw new UnauthorizedAccessException("Usuário não tem permissão para acessar este recurso");
    }
}