using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialService.Common;
using SocialService.Common.Interfaces;
using SocialService.Common.Models;
using SocialService.Common.Utils;
using SocialService.Friends.FriendRequest.CheckFriendRequestStatus;
using SocialService.Friends.FriendRequest.ManageFriendRequests;
using SocialService.Friends.FriendRequest.RemoveFriendRequest;
using SocialService.Friends.FriendRequest.SendFriendRequest;
using SocialService.Friends.Friendship.ListFriends;
using SocialService.Friends.Friendship.RemoveFriend;

namespace SocialService.Friends;

/// <summary>
///     Controller responsavel por gerenciar o perfil do usuario
/// </summary>
[ApiVersion("1.0")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("[Controller]/v{version:apiVersion}")]
public class FriendController : ControllerBase
{
    /// <summary>
    ///     Rota para enviar uma solicitação de amigo
    /// </summary>
    /// <returns></returns>
    [HttpPost("sendFriendRequest")]
    public async Task<IActionResult> SendFriendRequest([FromServices] IHandler<bool, SendFriendRequestCommand> handler,
        [FromBody] SendFriendRequestCommand requestCommand, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());

        return Ok(await handler.HandleAsync(requestCommand, cancellationToken));
    }

    /// <summary>
    ///     Rota para verificar o status de uma solicitação de amizade
    /// </summary>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("checkRequestStatus")]
    public async Task<IActionResult> CheckRequestStatus(
        [FromServices] IHandler<IEnumerable<CheckFriendRequestStatusViewModel>, CheckFriendRequestStatusQuery> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());

        return Ok(await handler.HandleAsync(new CheckFriendRequestStatusQuery(), cancellationToken));
    }

    /// <summary>
    ///     Rota para listar os amigos de um perfil
    /// </summary>
    /// <param name="profileId"></param>
    /// <param name="page"></param>
    /// <param name="rows"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("listFriends/{profileId:guid}")]
    public async Task<IActionResult> ViewProfile(Guid profileId, [FromQuery] int page, [FromQuery] int rows,
        [FromServices] IHandler<IEnumerable<ProfileBasicInformationViewModel>, ListFriendsQuery> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());

        ListFriendsQuery query = new();
        query.SetProfileId(profileId);
        query.SetPage(page);
        query.SetRows(rows);

        return Ok(await handler.HandleAsync(query, cancellationToken));
    }

    /// <summary>
    ///     Rota para gerenciar solicitações de amizade
    /// </summary>
    /// <returns></returns>
    [HttpPut("manageFriendRequests")]
    public async Task<IActionResult> ManageFriendRequests(
        [FromServices] IHandler<bool, ManageFriendRequestsCommand> handler,
        [FromBody] ManageFriendRequestsCommand command, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());

        return Ok(await handler.HandleAsync(command, cancellationToken));
    }

    /// <summary>
    ///     Rota para remover um amigo
    /// </summary>
    /// <returns></returns>
    [HttpDelete("removeFriend/{profileId:guid}")]
    public async Task<IActionResult> RemoveFriend(Guid profileId,
        [FromServices] IHandler<bool, RemoveFriendCommand> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());

        return Ok(await handler.HandleAsync(new RemoveFriendCommand(profileId), cancellationToken));
    }

    /// <summary>
    ///     Rota para remover uma solicitação de amizade
    /// </summary>
    /// <returns></returns>
    [HttpDelete("removeFriendRequest/{profileId:guid}")]
    public async Task<IActionResult> RemoveFriendRequest(Guid profileId,
        [FromServices] IHandler<bool, RemoveFriendRequestCommand> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());

        return Ok(await handler.HandleAsync(new RemoveFriendRequestCommand(profileId), cancellationToken));
    }
}