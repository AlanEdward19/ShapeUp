using Asp.Versioning;
using AuthService.Authentication.AuthenticateUser;
using AuthService.Common.Interfaces;
using AuthService.Permission.Common.Enums;
using AuthService.Permission.CreatePermission;
using AuthService.Permission.DeletePermission;
using AuthService.Permission.GetGroupPermissions;
using AuthService.Permission.GetUserPermissions;
using AuthService.Permission.GrantGroupPermission;
using AuthService.Permission.GrantUserPermission;
using AuthService.Permission.UpdatePermission;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Permission;

[ApiVersion("1.0")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("v{version:apiVersion}/[Controller]")]
public class PermissionController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreatePermission([FromBody] CreatePermissionCommand command,
        [FromServices] IHandler<bool, CreatePermissionCommand> handler, CancellationToken cancellationToken)
    {
        await handler.HandleAsync(command, cancellationToken);
        return Created();
    }
    
    [HttpDelete("{permissionId:guid}")]
    public async Task<IActionResult> CreatePermission(Guid permissionId,
        [FromServices] IHandler<bool, DeletePermissionCommand> handler, CancellationToken cancellationToken)
    {
        DeletePermissionCommand command = new(permissionId);
        await handler.HandleAsync(command, cancellationToken);
        
        return NoContent();
    }
    
    [HttpGet("/Group/{groupId:guid}/Permission")]
    public async Task<IActionResult> GetGroupPermissions(Guid groupId,
        [FromServices] IHandler<ICollection<PermissionDto>, GetGroupPermissionsQuery> handler, CancellationToken cancellationToken)
    {
        GetGroupPermissionsQuery query = new(groupId);
       
        return Ok( await handler.HandleAsync(query, cancellationToken));
    }
    
    [HttpGet("/User/{userId:guid}/Permission")]
    public async Task<IActionResult> GetUserPermissions(Guid userId,
        [FromServices] IHandler<ICollection<PermissionDto>, GetUserPermissionsQuery> handler, CancellationToken cancellationToken)
    {
        GetUserPermissionsQuery query = new(userId);
       
        return Ok( await handler.HandleAsync(query, cancellationToken));
    }
    
    [HttpPost("/Group/{groupId:guid}/Permission/{permissionId:guid}")]
    public async Task<IActionResult> GrantGroupPermission(Guid groupId, Guid permissionId,
        [FromServices] IHandler<bool, GrantGroupPermissionCommand> handler, CancellationToken cancellationToken)
    {
        GrantGroupPermissionCommand command = new(groupId, permissionId);
        await handler.HandleAsync(command, cancellationToken);
        
        return Created();
    }
    
    [HttpPost("/User/{userId:guid}/Permission/{permissionId:guid}")]
    public async Task<IActionResult> GrantUserPermission(Guid userId, Guid permissionId,
        [FromServices] IHandler<bool, GrantUserPermissionCommand> handler, CancellationToken cancellationToken)
    {
        GrantUserPermissionCommand command = new(userId, permissionId);
        await handler.HandleAsync(command, cancellationToken);
        
        return Created();
    }
    
    [HttpPatch("{permissionId:guid}")]
    public async Task<IActionResult> UpdatePermission(Guid permissionId, [FromBody]  UpdatePermissionCommand command,
        [FromServices] IHandler<bool, UpdatePermissionCommand> handler, CancellationToken cancellationToken)
    {
        command.SetPermissionId(permissionId);
        await handler.HandleAsync(command, cancellationToken);
        
        return Ok();
    }
}