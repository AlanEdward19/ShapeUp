using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using NutritionService.Common;
using NutritionService.Common.Interfaces;
using NutritionService.DailyMenu.CreateDailyMenu;
using NutritionService.DailyMenu.DeleteDailyMenu;
using NutritionService.DailyMenu.EditDailyMenu;
using NutritionService.DailyMenu.GetDailyMenuDetails;
using NutritionService.DailyMenu.ListDailyMenus;
using SharedKernel.Filters;
using SharedKernel.Utils;

namespace NutritionService.DailyMenu;


/// <summary>
/// Controlador para gerenciar o cardápio diário.
/// </summary>
[ApiVersion("1.0")]
[ApiController]
[TokenValidatorFilter]
[Route("v{version:apiVersion}/[Controller]")]
public class DailyMenuController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateDailyMenu(CreateDailyMenuCommand command,
        [FromServices] IHandler<DailyMenu, CreateDailyMenuCommand> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());
        
        // Validators
        
        return Created(HttpContext.Request.Path, await handler.HandleAsync(command, cancellationToken));
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDailyMenu(string id,
        [FromServices] IHandler<DailyMenu, DeleteDailyMenuCommand> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());
        
        //Validators

        await handler.HandleAsync(new DeleteDailyMenuCommand(id), cancellationToken);

        return NoContent();
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> EditDailyMenu(string id, [FromBody] EditDailyMenuCommand command,
        [FromServices] IHandler<DailyMenu, EditDailyMenuCommand> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());
        
        command.SetId(id);

        var result = await handler.HandleAsync(command, cancellationToken);
        
        //Validation
        
        return Ok(result);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetDailyMenuDetails(string id,
        [FromServices] IHandler<DailyMenu, GetDailyMenuDetailsQuery> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());
        
        //Validation

        return Ok(await handler.HandleAsync(new GetDailyMenuDetailsQuery(id), cancellationToken));
    }
    [HttpGet]
    public async Task<IActionResult> GetDailyMenus(
        [FromQuery] ListDailyMenuQuery query,
        [FromServices] IHandler<DailyMenu, ListDailyMenuQuery> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());
        
        //Validation

        return Ok(await handler.HandleAsync(query, cancellationToken));
    }
}