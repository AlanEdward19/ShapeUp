using Asp.Versioning;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialService.ActivityFeed.GetActivityFeed;
using SocialService.Common;
using SocialService.Common.Interfaces;
using SocialService.Common.Utils;

namespace SocialService.ActivityFeed;

/// <summary>
///     Controller responsavel por gerenciar funções relacionadas ao feed de atividades
/// </summary>
[ApiVersion("1.0")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("[Controller]/v{version:apiVersion}")]
public class ActivityFeedController : ControllerBase
{
    /// <summary>
    ///     Rota para seguir um perfil
    /// </summary>
    /// <param name="handler"></param>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("BuildActivityFeed")]
    public async Task<IActionResult> BuildActivityFeed([FromServices] IHandler<IEnumerable<Post.Post>, GetActivityFeedQuery> handler,
        GetActivityFeedQuery query, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());
        
        GetActivityFeedQueryValidator validator = new();
        await validator.ValidateAndThrowAsync(query, cancellationToken);

        return Ok(await handler.HandleAsync(query, cancellationToken));
    }
}