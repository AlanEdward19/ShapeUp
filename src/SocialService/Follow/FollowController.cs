using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialService.Common;
using SocialService.Common.Interfaces;
using SocialService.Common.Models;
using SocialService.Common.Utils;
using SocialService.Follow.FollowUser;
using SocialService.Follow.GetFollowers;
using SocialService.Follow.GetFollowing;
using SocialService.Follow.UnfollowUser;

namespace SocialService.Follow;

/// <summary>
///     Controller responsavel por gerenciar funções de seguir e deixar de seguir de um perfil
/// </summary>
[ApiVersion("1.0")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("[Controller]/v{version:apiVersion}")]
public class FollowController : ControllerBase
{
    /// <summary>
    ///     Rota para seguir um perfil
    /// </summary>
    /// <param name="handler"></param>
    /// <param name="profileId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("followUser/{profileId:guid}")]
    public async Task<IActionResult> FollowUser([FromServices] IHandler<bool, FollowUserCommand> handler,
        Guid profileId, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());

        return Ok(await handler.HandleAsync(new FollowUserCommand(profileId), cancellationToken));
    }

    /// <summary>
    ///     Rotar para deixar de seguir um perfil
    /// </summary>
    /// <param name="handler"></param>
    /// <param name="profileId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("unfollowUser/{profileId:guid}")]
    public async Task<IActionResult> UnfollowUser([FromServices] IHandler<bool, UnfollowUserCommand> handler,
        Guid profileId, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());

        return Ok(await handler.HandleAsync(new UnfollowUserCommand(profileId), cancellationToken));
    }

    /// <summary>
    ///     Rotar para obter os seguidores de um perfil
    /// </summary>
    /// <param name="profileId"></param>
    /// <param name="page"></param>
    /// <param name="rows"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("getFollowers/{profileId:guid}")]
    public async Task<IActionResult> GetFollowers(Guid profileId, [FromQuery] int page, [FromQuery] int rows,
        [FromServices] IHandler<IEnumerable<ProfileBasicInformationViewModel>, GetFollowersQuery> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());

        GetFollowersQuery query = new();
        query.SetProfileId(profileId);
        query.SetPage(page);
        query.SetRows(rows);

        return Ok(await handler.HandleAsync(query, cancellationToken));
    }

    /// <summary>
    ///     Rotar para obter os perfis que um perfil segue
    /// </summary>
    /// <param name="profileId"></param>
    /// <param name="page"></param>
    /// <param name="rows"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("getFollowing/{profileId:guid}")]
    public async Task<IActionResult> GetFollowing(Guid profileId, [FromQuery] int page, [FromQuery] int rows,
        [FromServices] IHandler<IEnumerable<ProfileBasicInformationViewModel>, GetFollowingQuery> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());

        GetFollowingQuery query = new();
        query.SetProfileId(profileId);
        query.SetPage(page);
        query.SetRows(rows);

        return Ok(await handler.HandleAsync(query, cancellationToken));
    }
}