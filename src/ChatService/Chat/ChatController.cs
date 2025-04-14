using Asp.Versioning;
using ChatService.Chat.Common;
using ChatService.Chat.GetMessages;
using ChatService.Chat.GetRecentMessages;
using ChatService.Chat.SendMessage;
using ChatService.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Filters;
using SharedKernel.Utils;

namespace ChatService.Chat;

/// <summary>
/// Controlador para lidar com as requisições relacionadas a chat
/// </summary>
[ApiVersion("1.0")]
[ApiController]
[TokenValidatorFilter]
[Route("v{version:apiVersion}/[Controller]")]
public class ChatController : ControllerBase
{
    /// <summary>
    /// Rota para obter as mensagens recentes
    /// </summary>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="page"></param>
    /// <returns></returns>
    [HttpGet("messages/getRecentMessages")]
    public async Task<IActionResult> GetRecentMessages([FromServices] IHandler<IEnumerable<ChatMessage>,
        GetRecentMessagesQuery> handler, CancellationToken cancellationToken, [FromQuery] int page = 1)
    {
        GetRecentMessagesQuery query = new();
        string profileId = User.GetObjectId();
        query.SetProfileId(profileId);
        query.SetPage(page);

        return Ok(await handler.HandleAsync(query, cancellationToken));
    }
    
    /// <summary>
    /// Rota para obter as mensagens entre dois perfis
    /// </summary>
    /// <param name="profileBId"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="page"></param>
    /// <returns></returns>
    [HttpGet("messages/getMessages/{profileBId}")]
    public async Task<IActionResult> GetMessages(string profileBId, [FromServices] IHandler<IEnumerable<ChatMessage>,
        GetMessagesQuery> handler, CancellationToken cancellationToken, [FromQuery] int page = 1)
    {
        GetMessagesQuery query = new();
        string profileId = User.GetObjectId();
        query.SetProfileAId(profileId);
        query.SetProfileBId(profileBId);
        query.SetPage(page);

        return Ok(await handler.HandleAsync(query, cancellationToken));
    }

    /// <summary>
    /// Rota para enviar uma mensagem
    /// </summary>
    /// <param name="command"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("messages/send")]
    public async Task<IActionResult> SendMessage([FromBody] SendMessageCommand command,
        [FromServices] IHandler<bool, SendMessageCommand> handler,
        CancellationToken cancellationToken)
    {
        string profileId = User.GetObjectId();
        command.SetSenderId(profileId);

        bool result = await handler.HandleAsync(command, cancellationToken);

        if (result)
            return Accepted();

        return BadRequest();
    }
}