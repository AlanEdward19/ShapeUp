﻿using SocialService.Common.ValueObjects;
using SocialService.Follow.FollowUser;
using SocialService.Follow.GetFollowers;
using SocialService.Follow.GetFollowing;
using SocialService.Follow.UnfollowUser;
using SocialService.Post;
using SocialService.Profile.Common.Repository;

namespace SocialService.Follow;

/// <summary>
///     Controller responsavel por gerenciar funções de seguir e deixar de seguir de um perfil
/// </summary>
[ApiVersion("1.0")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("v{version:apiVersion}/[Controller]")]
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
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> FollowUser([FromServices] IHandler<bool, FollowUserCommand> handler,
        Guid profileId, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());

        FollowUserCommand command = new(profileId);

        FollowUserCommandValidator validator = new(repository);
        await validator.ValidateAndThrowAsync(command, cancellationToken);

        await handler.HandleAsync(command, cancellationToken);
        return Created();
    }

    /// <summary>
    ///     Rotar para deixar de seguir um perfil
    /// </summary>
    /// <param name="handler"></param>
    /// <param name="profileId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("unfollowUser/{profileId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UnfollowUser([FromServices] IHandler<bool, UnfollowUserCommand> handler,
        Guid profileId, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());

        UnfollowUserCommand command = new(profileId);

        UnfollowUserCommandValidator validator = new(repository);
        await validator.ValidateAndThrowAsync(command, cancellationToken);

        await handler.HandleAsync(command, cancellationToken);
        return NoContent();
    }

    /// <summary>
    ///     Rotar para obter os seguidores de um perfil
    /// </summary>
    /// <param name="profileId"></param>
    /// <param name="queryParameters"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("getFollowers/{profileId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type =  typeof(IEnumerable<ProfileBasicInformation>))]
    public async Task<IActionResult> GetFollowers(Guid profileId, [FromQuery] BaseQueryParametersValueObject queryParameters,
        [FromServices] IHandler<IEnumerable<ProfileBasicInformation>, GetFollowersQuery> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());

        GetFollowersQuery query = new();
        query.SetProfileId(profileId);
        query.SetPage(queryParameters.Page);
        query.SetRows(queryParameters.Rows);

        GetFollowersQueryValidator validator = new(repository);
        await validator.ValidateAndThrowAsync(query, cancellationToken);

        return Ok(await handler.HandleAsync(query, cancellationToken));
    }

    /// <summary>
    ///     Rotar para obter os perfis que um perfil segue
    /// </summary>
    /// <param name="profileId"></param>
    /// <param name="queryParameters"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("getFollowing/{profileId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type =  typeof(IEnumerable<ProfileBasicInformation>))]
    public async Task<IActionResult> GetFollowing(Guid profileId, [FromQuery] BaseQueryParametersValueObject queryParameters,
        [FromServices] IHandler<IEnumerable<ProfileBasicInformation>, GetFollowingQuery> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());

        GetFollowingQuery query = new();
        query.SetProfileId(profileId);
        query.SetPage(queryParameters.Page);
        query.SetRows(queryParameters.Rows);

        GetFollowingQueryValidator validator = new(repository);
        await validator.ValidateAndThrowAsync(query, cancellationToken);

        return Ok(await handler.HandleAsync(query, cancellationToken));
    }
}