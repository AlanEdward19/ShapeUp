using Asp.Versioning;
using AuthService.Authentication.EnhanceToken;
using AuthService.Common;
using AuthService.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Enums;
using SharedKernel.Filters;
using SharedKernel.Utils;

namespace AuthService.Authentication;

[ApiVersion("1.0")]
[ApiController]
[TokenValidatorFilter]
[Route("v{version:apiVersion}/[Controller]")]
public class AuthenticationController : ControllerBase
{
    [HttpPost("enhanceToken")]
    public async Task<IActionResult> EnhanceToken([FromBody] EnhanceTokenCommand command,
        [FromServices] IHandler<bool, EnhanceTokenCommand> handler, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        
        await handler.HandleAsync(command, cancellationToken);

        return Created();
    }
    
    [HttpPost("/v{version:apiVersion}/User/{userId}/enhanceToken")]
    [AuthFilter(EPermissionAction.Write, "permission")]
    public async Task<IActionResult> EnhanceUserToken(string userId, [FromBody] EnhanceTokenCommand command,
        [FromServices] IHandler<bool, EnhanceTokenCommand> handler, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = userId;
        
        await handler.HandleAsync(command, cancellationToken);

        return Created();
    }
}