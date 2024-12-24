using Asp.Versioning;
using FluentValidation;
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
using SocialService.Profile.Common.Repository;

namespace SocialService.Follow;

/// <summary>
///     Controller responsavel por gerenciar funções de seguir e deixar de seguir de um perfil
/// </summary>
[ApiVersion("1.0")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("[Controller]/v{version:apiVersion}")]
public class FollowController(IProfileGraphRepository repository) : ControllerBase
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

        FollowUserCommand command = new(profileId);

        FollowUserCommandValidator validator = new(repository);
        await validator.ValidateAndThrowAsync(command, cancellationToken);

        return Ok(await handler.HandleAsync(command, cancellationToken));
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

        UnfollowUserCommand command = new(profileId);

        UnfollowUserCommandValidator validator = new(repository);
        await validator.ValidateAndThrowAsync(command, cancellationToken);

        return Ok(await handler.HandleAsync(command, cancellationToken));
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
        [FromServices] IHandler<IEnumerable<ProfileBasicInformation>, GetFollowersQuery> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());

        GetFollowersQuery query = new();
        query.SetProfileId(profileId);
        query.SetPage(page);
        query.SetRows(rows);
        
        GetFollowersQueryValidator validator = new(repository);
        await validator.ValidateAndThrowAsync(query, cancellationToken);

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
        [FromServices] IHandler<IEnumerable<ProfileBasicInformation>, GetFollowingQuery> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());

        GetFollowingQuery query = new();
        query.SetProfileId(profileId);
        query.SetPage(page);
        query.SetRows(rows);
        
        GetFollowingQueryValidator validator = new(repository);
        await validator.ValidateAndThrowAsync(query, cancellationToken);

        return Ok(await handler.HandleAsync(query, cancellationToken));
    }
}