using Asp.Versioning;
using AuthService.Common;
using AuthService.Common.Interfaces;
using AuthService.Connections.Database;
using AuthService.Permission.CreatePermission;
using AuthService.Permission.DeletePermission;
using AuthService.Permission.GetGroupPermissions;
using AuthService.Permission.GetUserPermissions;
using AuthService.Permission.GrantGroupPermission;
using AuthService.Permission.GrantUserPermission;
using AuthService.Permission.UpdatePermission;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Enums;
using SharedKernel.Filters;
using SharedKernel.Utils;

namespace AuthService.Permission;

[ApiVersion("1.0")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("v{version:apiVersion}/[Controller]")]
public class PermissionController(AuthDbContext dbContext) : ControllerBase
{
    [HttpPost]
    [AuthFilter(EPermissionAction.Write, "permission")]
    public async Task<IActionResult> CreatePermission([FromBody] CreatePermissionCommand command,
        [FromServices] IHandler<bool, CreatePermissionCommand> handler, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();

        CreatePermissionCommandValidator validator = new();
        await validator.ValidateAndThrowAsync(command, cancellationToken);
        
        await handler.HandleAsync(command, cancellationToken);
        return Created();
    }
    
    [HttpDelete("{permissionId:guid}")]
    [AuthFilter(EPermissionAction.Delete, "permission")]
    public async Task<IActionResult> DeletePermission(Guid permissionId,
        [FromServices] IHandler<bool, DeletePermissionCommand> handler, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        
        DeletePermissionCommand command = new(permissionId);
        DeletePermissionCommandValidator validator = new(dbContext);
        await validator.ValidateAndThrowAsync(command, cancellationToken);
        
        await handler.HandleAsync(command, cancellationToken);
        
        return NoContent();
    }
    
    [HttpGet("/Group/{groupId:guid}/Permission")]
    [AuthFilter(EPermissionAction.Read, "permission")]
    public async Task<IActionResult> GetGroupPermissions(Guid groupId,
        [FromServices] IHandler<ICollection<PermissionDto>, GetGroupPermissionsQuery> handler, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        
        GetGroupPermissionsQuery query = new(groupId);
        GetGroupPermissionsQueryValidator validator = new(dbContext);
        await validator.ValidateAndThrowAsync(query, cancellationToken);
       
        return Ok( await handler.HandleAsync(query, cancellationToken));
    }
    
    [HttpGet("/User/{userId}/Permission")]
    [AuthFilter(EPermissionAction.Read, "permission")]
    public async Task<IActionResult> GetUserPermissions(string userId,
        [FromServices] IHandler<ICollection<PermissionDto>, GetUserPermissionsQuery> handler, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        
        GetUserPermissionsQuery query = new(userId);
        GetUserPermissionsQueryValidator validator = new(dbContext);
        await validator.ValidateAndThrowAsync(query, cancellationToken);
       
        return Ok( await handler.HandleAsync(query, cancellationToken));
    }
    
    [HttpPost("/Group/{groupId:guid}/Permission/{permissionId:guid}")]
    [AuthFilter(EPermissionAction.Write, "permission")]
    public async Task<IActionResult> GrantGroupPermission(Guid groupId, Guid permissionId,
        [FromServices] IHandler<bool, GrantGroupPermissionCommand> handler, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        
        GrantGroupPermissionCommand command = new(groupId, permissionId);
        GrantGroupPermissionCommandValidator validator = new(dbContext);
        await validator.ValidateAndThrowAsync(command, cancellationToken);
        
        await handler.HandleAsync(command, cancellationToken);
        
        return Created();
    }
    
    [HttpPost("/User/{userId}/Permission/{permissionId:guid}")]
    [AuthFilter(EPermissionAction.Write, "permission")]
    public async Task<IActionResult> GrantUserPermission(string userId, Guid permissionId,
        [FromServices] IHandler<bool, GrantUserPermissionCommand> handler, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        
        GrantUserPermissionCommand command = new(userId, permissionId);
        GrantUserPermissionCommandValidator validator = new(dbContext);
        await validator.ValidateAndThrowAsync(command, cancellationToken);
        
        await handler.HandleAsync(command, cancellationToken);
        
        return Created();
    }
    
    [HttpPatch("{permissionId:guid}")]
    [AuthFilter(EPermissionAction.Update, "permission")]
    public async Task<IActionResult> UpdatePermission(Guid permissionId, [FromBody]  UpdatePermissionCommand command,
        [FromServices] IHandler<bool, UpdatePermissionCommand> handler, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        
        command.SetPermissionId(permissionId);
        UpdatePermissionCommandValidator validator = new(dbContext);
        await validator.ValidateAndThrowAsync(command, cancellationToken);
        
        await handler.HandleAsync(command, cancellationToken);
        
        return Ok();
    }
}