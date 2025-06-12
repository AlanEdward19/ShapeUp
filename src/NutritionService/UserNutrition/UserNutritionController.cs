using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using NutritionService.Common;
using NutritionService.Common.Interfaces;
using NutritionService.UserNutrition.CreateUserNutrition;
using NutritionService.UserNutrition.DeleteUserNutrition;
using NutritionService.UserNutrition.EditUserNutrition;
using NutritionService.UserNutrition.GetUserNutritionDetails;
using NutritionService.UserNutrition.ListUserNutritions;
using SharedKernel.Filters;
using SharedKernel.Utils;
using ProfileContext = SharedKernel.Utils.ProfileContext;

namespace NutritionService.UserNutrition;

[ApiVersion("1.0")]
[ApiController]
[TokenValidatorFilter]
[Route("v{version:apiVersion}/[Controller]")]
public class UserNutritionController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateUserNutrition(CreateUserNutritionCommand command,
        [FromServices] IHandler<UserNutrition, CreateUserNutritionCommand> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        //Validation
        
        return Ok(await handler.HandleAsync(command, cancellationToken));
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserNutrition(string id,
        [FromServices] IHandler<UserNutrition, DeleteUserNutritionCommand> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        
        //Validation

        await handler.HandleAsync(new DeleteUserNutritionCommand(id), cancellationToken);
        
        return NoContent();
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> EditUserNutrition(string id,
        [FromBody] EditUserNutritionCommand command,
        [FromServices] IHandler<UserNutrition, EditUserNutritionCommand> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        command.SetId(id);
        
        //Validation
        
        return Ok(await handler.HandleAsync(command, cancellationToken));
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserNutrition(string id,
        [FromServices] IHandler<UserNutrition, GetUserNutritionDetailsQuery> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        
        //Validation
        
        return Ok(await handler.HandleAsync(new GetUserNutritionDetailsQuery(id), cancellationToken));
    }
    
    [HttpGet]
    public async Task<IActionResult> ListUserNutritions([FromQuery] ListUserNutritionsQuery query,
        [FromServices] IHandler<IEnumerable<UserNutrition>, ListUserNutritionsQuery> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        
        //Validation
        
        return Ok(await handler.HandleAsync(query, cancellationToken));
    }
}