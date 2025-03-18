using AuthService.Authentication.AuthenticateUser;
using AuthService.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Authentication;

public class AuthenticationController : ControllerBase
{
    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate([FromBody] AuthenticateUserCommand command,
        [FromServices] IHandler<string, AuthenticateUserCommand> handler, CancellationToken cancellationToken)
    {
        var token = await handler.HandleAsync(command, cancellationToken);
        return Ok(new { Token = token });
    }
}