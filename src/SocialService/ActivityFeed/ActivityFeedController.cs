using SharedKernel.Filters;
using SharedKernel.Utils;
using SocialService.ActivityFeed.GetActivityFeed;
using SocialService.Post;

namespace SocialService.ActivityFeed;

/// <summary>
///     Controller responsavel por gerenciar funções relacionadas ao feed de atividades
/// </summary>
[ApiVersion("1.0")]
[ApiController]
[TokenValidatorFilter]
[Route("v{version:apiVersion}/[Controller]")]
public class ActivityFeedController : ControllerBase
{
    /// <summary>
    ///     Rota para construir o feed de atividades
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
        ProfileContext.ProfileId = User.GetObjectId();

        GetActivityFeedQueryValidator validator = new();
        await validator.ValidateAndThrowAsync(query, cancellationToken);

        return Ok(await handler.HandleAsync(query, cancellationToken));
    }
}