using SocialService.ActivityFeed.GetActivityFeed;
using SocialService.Post;

namespace SocialService.ActivityFeed;

/// <summary>
///     Controller responsavel por gerenciar funções relacionadas ao feed de atividades
/// </summary>
[ApiVersion("1.0")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("v{version:apiVersion}/[Controller]")]
public class ActivityFeedController : ControllerBase
{
    /// <summary>
    ///     Rota para seguir um perfil
    /// </summary>
    /// <param name="handler"></param>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("buildActivityFeed")]
    [ProducesResponseType(StatusCodes.Status200OK, Type =  typeof(IEnumerable<PostDto>))]
    public async Task<IActionResult> BuildActivityFeed(
        [FromServices] IHandler<IEnumerable<PostDto>, GetActivityFeedQuery> handler,
        [FromQuery] GetActivityFeedQuery query, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());

        GetActivityFeedQueryValidator validator = new();
        await validator.ValidateAndThrowAsync(query, cancellationToken);

        return Ok(await handler.HandleAsync(query, cancellationToken));
    }
}