using Asp.Versioning;
using AuthService.Common;
using AuthService.Common.Interfaces;
using AuthService.Common.User;
using AuthService.Group.AddUserToGroup;
using AuthService.Group.ChangeUserRoleInGroup;
using AuthService.Group.Common.Enums;
using AuthService.Group.CreateGroup;
using AuthService.Group.DeleteGroup;
using AuthService.Group.GetUsersFromGroup;
using AuthService.Group.RemoveUserFromGroup;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Enums;
using SharedKernel.Filters;
using SharedKernel.Utils;

namespace AuthService.Group;

[ApiVersion("1.0")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("v{version:apiVersion}/[Controller]")]
public class GroupController : ControllerBase
{
    [HttpPost("{groupId:guid}/Users/{userId:guid}")]
    public async Task<IActionResult> AddUserToGroup(Guid groupId, Guid userId, [FromBody] EGroupRole role,
        [FromServices] IHandler<string, AddUserToGroupCommand> handler, CancellationToken cancellationToken)
    {
        AddUserToGroupCommand command = new(groupId, userId, role);
        var token = await handler.HandleAsync(command, cancellationToken);
        return Ok(new { Token = token });
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateGroup([FromBody] CreateGroupCommand command,
        [FromServices] IHandler<bool, CreateGroupCommand> handler, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());
        
        await handler.HandleAsync(command, cancellationToken);

        return Created();
    }
    
    [HttpDelete("{groupId:guid}")]
    public async Task<IActionResult> DeleteGroup(Guid groupId,
        [FromServices] IHandler<bool, DeleteGroupCommand> handler, CancellationToken cancellationToken)
    {
        DeleteGroupCommand command = new(groupId);
        await handler.HandleAsync(command, cancellationToken);

        return NoContent();
    }
    
    [HttpGet("{groupId:guid}/Users")]
    public async Task<IActionResult> GetUsersFromGroup(Guid groupId,
        [FromServices] IHandler<ICollection<UserDto>, GetUsersFromGroupQuery> handler, CancellationToken cancellationToken)
    {
        GetUsersFromGroupQuery query = new(groupId);
        return Ok(await handler.HandleAsync(query, cancellationToken));
    }
    
    [HttpDelete("{groupId:guid}/Users/{userId:guid}")]
    public async Task<IActionResult> RemoveUserFromGroup(Guid groupId, Guid userId,
        [FromServices] IHandler<bool, RemoveUserFromGroupCommand> handler, CancellationToken cancellationToken)
    {
        RemoveUserFromGroupCommand command = new(groupId, userId);
        await handler.HandleAsync(command, cancellationToken);

        return NoContent();
    }
    
    [HttpPut("{groupId:guid}/Users/{userId:guid}")]
    public async Task<IActionResult> ChangeUserRoleInGroup(Guid groupId, Guid userId, [FromBody] EGroupRole role,
        [FromServices] IHandler<bool, ChangeUserRoleInGroupCommand> handler, CancellationToken cancellationToken)
    {
        ChangeUserRoleInGroupCommand command = new(groupId, userId, role);
        await handler.HandleAsync(command, cancellationToken);

        return Ok();
    }
}