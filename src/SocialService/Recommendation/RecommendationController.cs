using SocialService.Recommendation.GetFriendRecommendations;

namespace SocialService.Recommendation;

/// <summary>
///     Controller responsavel por gerenciar as recomendações de amigos
/// </summary>
[ApiVersion("1.0")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("v{version:apiVersion}/[Controller]")]
public class RecommendationController : ControllerBase
{
    /// <summary>
    ///     Rota para receber recomendações de amigos
    /// </summary>
    /// <returns></returns>
    [HttpGet("friendRecommendations")]
    public async Task<IActionResult> GetFriendRecommendations(
        [FromServices] IHandler<IEnumerable<FriendRecommendation>, GetFriendRecommendationQuery> handler,
        CancellationToken cancellationToken)
    {
        GetFriendRecommendationQuery query = new();
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());

        GetFriendRecommendationQueryValidator validator = new();
        await validator.ValidateAndThrowAsync(query, cancellationToken);

        return Ok(await handler.HandleAsync(query, cancellationToken));
    }
}