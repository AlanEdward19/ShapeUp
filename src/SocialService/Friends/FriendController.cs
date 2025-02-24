using SocialService.Friends.Common.Repository;
using SocialService.Friends.FriendRequest.CheckFriendRequestStatus;
using SocialService.Friends.FriendRequest.ManageFriendRequests;
using SocialService.Friends.FriendRequest.RemoveFriendRequest;
using SocialService.Friends.FriendRequest.SendFriendRequest;
using SocialService.Friends.Friendship.ListFriends;
using SocialService.Friends.Friendship.RemoveFriend;
using SocialService.Profile.Common.Repository;

namespace SocialService.Friends;

/// <summary>
///     Controller responsavel por gerenciar o perfil do usuario
/// </summary>
[ApiVersion("1.0")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("v{version:apiVersion}/[Controller]")]
public class FriendController(IProfileGraphRepository profileGraphRepository, IFriendshipGraphRepository repository)
    : ControllerBase
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

        SendFriendRequestCommandValidator validator = new(profileGraphRepository);
        await validator.ValidateAndThrowAsync(requestCommand, cancellationToken);

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
        [FromServices] IHandler<IEnumerable<ProfileBasicInformation>, ListFriendsQuery> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());

        ListFriendsQuery query = new();
        query.SetProfileId(profileId);
        query.SetPage(page);
        query.SetRows(rows);

        ListFriendsQueryValidator validator = new(profileGraphRepository);
        await validator.ValidateAndThrowAsync(query, cancellationToken);

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

        ManageFriendRequestsCommandValidator validator = new(profileGraphRepository, repository);
        await validator.ValidateAndThrowAsync(command, cancellationToken);

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

        RemoveFriendCommand command = new(profileId);

        RemoveFriendCommandValidator validator = new(profileGraphRepository);
        await validator.ValidateAndThrowAsync(command, cancellationToken);

        return Ok(await handler.HandleAsync(command, cancellationToken));
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

        RemoveFriendRequestCommand command = new(profileId);

        RemoveFriendRequestCommandValidator validator = new(profileGraphRepository, repository);
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        return Ok(await handler.HandleAsync(command, cancellationToken));
    }
}