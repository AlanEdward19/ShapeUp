using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using NutritionService.Common;
using NutritionService.Common.Interfaces;
using NutritionService.PublicFood.CreatePublicFood;
using NutritionService.PublicFood.DeletePublicFood;
using NutritionService.PublicFood.EditPublicFood;
using NutritionService.PublicFood.GetPublicFoodDetails;
using NutritionService.PublicFood.ListUnrevisedPublicFoods;
using NutritionService.UserFood;
using NutritionService.UserFood.CreateUserFood;
using SharedKernel.Filters;
using SharedKernel.Utils;

namespace NutritionService.PublicFood;


[ApiVersion("1.0")]
[ApiController]
[TokenValidatorFilter]
[Route("v{version:apiVersion}/[Controller]")]
public class PublicFoodController : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPublicFoodDetails(string id,
        [FromServices] IHandler<Food, GetPublicFoodDetailsQuery> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());

        return Ok(await handler.HandleAsync(new GetPublicFoodDetailsQuery(id), cancellationToken));
    }
    
    [HttpGet("listUnrevisedFoods")]
    public async Task<IActionResult> ListUnrevisedPublicFoods([FromQuery] ListUnrevisedPublicFoodsQuery query,
        [FromServices] IHandler<IEnumerable<Food>, ListUnrevisedPublicFoodsQuery> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());

        return Ok(await handler.HandleAsync(query, cancellationToken));
    }
    
    [HttpPost]
    public async Task<IActionResult> CreatePublicFood([FromBody] CreatePublicFoodCommand command,
        [FromServices] IHandler<Food, CreatePublicFoodCommand> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());
        command.SetCreatedBy(ProfileContext.ProfileId);
        

        return Ok(await handler.HandleAsync(command, cancellationToken));
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> EditPublicFood(string id, [FromBody] EditPublicFoodCommand command,
        [FromServices] IHandler<Food, EditPublicFoodCommand> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());
        command.SetId(id);

        return Ok(await handler.HandleAsync(command, cancellationToken));
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePublicFood(string id,
        [FromServices] IHandler<Food, DeletePublicFoodCommand> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());
        var command = new DeletePublicFoodCommand(id);
        
        var result = await handler.HandleAsync(command, cancellationToken);

        return NoContent();
    }
}