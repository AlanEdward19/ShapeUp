﻿using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using NotificationService.Common.Interfaces;
using NotificationService.Notification.SendNotification;
using SharedKernel.Filters;

namespace NotificationService.Notification;

/// <summary>
/// Controller responsavel por gerenciar notificações
/// </summary>
[ApiVersion("1.0")]
[ApiController]
[TokenValidatorFilter]
[Route("v{version:apiVersion}/[Controller]")]
public class NotificationController : ControllerBase
{
    /// <summary>
    /// Rota para enviar uma notificação
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="handler"></param>
    /// <returns></returns>
    [HttpPost("sendNotification")]
    public async Task<IActionResult> SendNotification([FromBody] SendNotificationCommand command,
        CancellationToken cancellationToken, [FromServices] IHandler<bool, SendNotificationCommand> handler)
    {
        Guid profileId = Guid.Parse(User.GetObjectId());
        command.SetProfileId(profileId);
        
        await handler.HandleAsync(command, cancellationToken);
        
        return Ok(new { Status = "Notification Sent" });
    }
}