using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using NotificationService.Common.Interfaces;
using NotificationService.User.UserLoggedIn;
using NotificationService.User.UserSignOut;
using SharedKernel.Filters;
using SharedKernel.Utils;

namespace NotificationService.User;

/// <summary>
/// Controller responsável por gerenciar operações relacionadas ao usuário
/// </summary>
[ApiVersion("1.0")]
[ApiController]
[TokenValidatorFilter]
[Route("v{version:apiVersion}/[Controller]")]
public class UserController : ControllerBase
{
    /// <summary>
    /// Rota para registrar o login do usuário
    /// </summary>
    /// <param name="command"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("LogIn")]
    public async Task<IActionResult> UserLogIn([FromBody] UserLoggedInCommand command,
        [FromServices] IHandler<bool, UserLoggedInCommand> handler, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        command.SetUserId(ProfileContext.ProfileId);
        
        await handler.HandleAsync(command, cancellationToken);
        return Ok();
    }
    
    /// <summary>
    /// Rota para remover o dispositivo do usuário
    /// </summary>
    /// <param name="command"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("SignOut")]
    public async Task<IActionResult> UserSignOut([FromBody] UserSignOutCommand command,
        [FromServices] IHandler<bool, UserSignOutCommand> handler, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        command.SetUserId(ProfileContext.ProfileId);
        
        await handler.HandleAsync(command, cancellationToken);
        return Ok();
    }
}