﻿using Asp.Versioning;
using AuthService.Common;
using AuthService.Common.Interfaces;
using AuthService.Common.User;
using AuthService.Connections.Database;
using AuthService.Group.AddUserToGroup;
using AuthService.Group.ChangeUserRoleInGroup;
using AuthService.Group.Common.Enums;
using AuthService.Group.CreateGroup;
using AuthService.Group.DeleteGroup;
using AuthService.Group.GetUsersFromGroup;
using AuthService.Group.RemoveUserFromGroup;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Enums;
using SharedKernel.Filters;
using SharedKernel.Utils;

namespace AuthService.Group;

[ApiVersion("1.0")]
[ApiController]
[TokenValidatorFilter]
[Route("v{version:apiVersion}/[Controller]")]
public class GroupController(AuthDbContext dbContext) : ControllerBase
{
    [HttpPost("{groupId:guid}/Users/{userId}")]
    public async Task<IActionResult> AddUserToGroup(Guid groupId, string userId, [FromBody] EGroupRole role,
        [FromServices] IHandler<bool, AddUserToGroupCommand> handler, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        
        AddUserToGroupCommand command = new(groupId, userId, role);
        await handler.HandleAsync(command, cancellationToken);

        AddUserToGroupCommandValidator validator = new(dbContext);
        await validator.ValidateAndThrowAsync(command, cancellationToken);

        return Created();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateGroup([FromBody] CreateGroupCommand command,
        [FromServices] IHandler<bool, CreateGroupCommand> handler, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        await handler.HandleAsync(command, cancellationToken);

        return Created();
    }
    
    [HttpDelete("{groupId:guid}")]
    public async Task<IActionResult> DeleteGroup(Guid groupId,
        [FromServices] IHandler<bool, DeleteGroupCommand> handler, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        
        DeleteGroupCommand command = new(groupId);
        DeleteGroupCommandValidator validator = new(dbContext);
        await validator.ValidateAndThrowAsync(command, cancellationToken);
        
        await handler.HandleAsync(command, cancellationToken);

        return NoContent();
    }
    
    [HttpGet("{groupId:guid}/Users")]
    public async Task<IActionResult> GetUsersFromGroup(Guid groupId,
        [FromServices] IHandler<ICollection<UserDto>, GetUsersFromGroupQuery> handler, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        
        GetUsersFromGroupQuery query = new(groupId);
        GetUsersFromGroupQueryValidator validator = new(dbContext);
        await validator.ValidateAndThrowAsync(query, cancellationToken);
        
        return Ok(await handler.HandleAsync(query, cancellationToken));
    }
    
    [HttpDelete("{groupId:guid}/Users/{userId}")]
    public async Task<IActionResult> RemoveUserFromGroup(Guid groupId, string userId,
        [FromServices] IHandler<bool, RemoveUserFromGroupCommand> handler, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        
        RemoveUserFromGroupCommand command = new(groupId, userId);
        RemoveUserFromGroupCommandValidator validator = new(dbContext);
        await validator.ValidateAndThrowAsync(command, cancellationToken);
        
        await handler.HandleAsync(command, cancellationToken);

        return NoContent();
    }
    
    [HttpPut("{groupId:guid}/Users/{userId}")]
    public async Task<IActionResult> ChangeUserRoleInGroup(Guid groupId, string userId, [FromBody] EGroupRole role,
        [FromServices] IHandler<bool, ChangeUserRoleInGroupCommand> handler, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        
        ChangeUserRoleInGroupCommand command = new(groupId, userId, role);
        ChangeUserRoleInGroupCommandValidator validator = new(dbContext);
        await validator.ValidateAndThrowAsync(command, cancellationToken);
        
        await handler.HandleAsync(command, cancellationToken);

        return Ok();
    }
}