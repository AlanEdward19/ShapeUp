using Asp.Versioning;
using ChatService.Chat.GetRecentMessages;
using ChatService.Chat.SendMessage;
using ChatService.Common.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatService.Chat;

[ApiVersion("1.0")]
[ApiController]
// [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("[Controller]/v{version:apiVersion}")]
public class ChatController : ControllerBase
{
    [HttpGet("messages/getRecentMessages/{profileId}")]
    public async Task<IActionResult> GetRecentMessages(Guid profileId,
        [FromServices] IHandler<IEnumerable<ChatMessage>, GetRecentMessagesQuery> handler,
        CancellationToken cancellationToken,
        [FromQuery] int page = 1)
    {
        GetRecentMessagesQuery query = new();
        query.SetProfileId(profileId);
        query.SetPage(page);

        return Ok(await handler.HandleAsync(query, cancellationToken));
    }

    [HttpPost("messages/send")]
    public async Task<IActionResult> SendMessage([FromBody] SendMessageCommand command,
        [FromServices] IHandler<bool, SendMessageCommand> handler,
        CancellationToken cancellationToken)
    {
        bool result = await handler.HandleAsync(command, cancellationToken);

        if (result)
            return Accepted();

        return BadRequest();
    }
}