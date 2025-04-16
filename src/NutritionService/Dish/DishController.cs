using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using NutritionService.Common;
using NutritionService.Common.Interfaces;
using NutritionService.Dish.CreateDish;
using NutritionService.Dish.DeleteDish;
using NutritionService.Dish.EditDish;
using NutritionService.Dish.GetDishDetails;
using SharedKernel.Filters;
using SharedKernel.Utils;

namespace NutritionService.Dish;


/// <summary>
/// Controller para o gerenciamento de pratos
/// </summary>
[ApiVersion("1.0")]
[ApiController]
[TokenValidatorFilter]
[Route("v{version:apiVersion}/[Controller]")]
public class DishController : ControllerBase
{
    /// <summary>
    /// Rota para obter detalhes de um prato
    /// </summary>
    /// <param name="id"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("getDishDetails/{id}")]
    public async Task<IActionResult> GetDishDetails(string id,
        [FromServices] IHandler<Dish, GetDishDetailsQuery> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());
        
        //Validation

        return Ok(await handler.HandleAsync(new GetDishDetailsQuery(id), cancellationToken));
    }
    
    /// <summary>
    /// Rota para criar um prato
    /// </summary>
    /// <param name="command"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Post(CreateDishCommand command,
        [FromServices] IHandler<Dish, CreateDishCommand> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());

        var result = await handler.HandleAsync(command, cancellationToken);
        
        //Validation
        
        return Created(HttpContext.Request.Path, await handler.HandleAsync(command, cancellationToken));
    }
    
    /// <summary>
    /// Rota para atualizar um prato
    /// </summary>
    /// <param name="command"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("update")]
    public async Task<IActionResult> Put(EditDishCommand command,
        [FromServices] IHandler<Dish, EditDishCommand> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());

        var result = await handler.HandleAsync(command, cancellationToken);
        
        //Validation
        
        return Ok(result);
    }
    
    /// <summary>
    /// Rota para deletar um prato
    /// </summary>
    /// <param name="id"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(string id,
        [FromServices] IHandler<Dish, DeleteDishCommand> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());

        var result = await handler.HandleAsync(new DeleteDishCommand(id), cancellationToken);
        
        //Validation

        return NoContent();
    }
}