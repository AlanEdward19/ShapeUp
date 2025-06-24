using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using NutritionService.Common;
using NutritionService.Common.Interfaces;
using NutritionService.Dish.CreateDish;
using NutritionService.Dish.DeleteDish;
using NutritionService.Dish.EditDish;
using NutritionService.Dish.GetDishDetails;
using NutritionService.Dish.ListDishes;
using SharedKernel.Filters;
using SharedKernel.Utils;
using ProfileContext = SharedKernel.Utils.ProfileContext;

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
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(DishDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDishDetails(string id,
        [FromServices] IHandler<DishDto, GetDishDetailsQuery> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        
        //Validation

        return Ok(await handler.HandleAsync(new GetDishDetailsQuery(id), cancellationToken));
    }
    
    
    /// <summary>
    /// Rota para listar pratos
    /// </summary>
    /// <param name="query"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<DishDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListDishes([FromQuery] ListDishesQuery query,
        [FromServices] IHandler<IEnumerable<DishDto>, ListDishesQuery> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        
        //Validation

        return Ok(await handler.HandleAsync(query, cancellationToken));
    }
    
    /// <summary>
    /// Rota para criar um prato
    /// </summary>
    /// <param name="command"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DishDto))]
    public async Task<IActionResult> CreateDish(CreateDishCommand command,
        [FromServices] IHandler<DishDto, CreateDishCommand> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        //Validation
        
        return Created(HttpContext.Request.Path , await handler.HandleAsync(command, cancellationToken));
    }

    /// <summary>
    /// Rota para atualizar um prato
    /// </summary>
    /// <param name="id"></param>
    /// <param name="command"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> EditDish(string id, EditDishCommand command,
        [FromServices] IHandler<bool, EditDishCommand> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        
        command.SetId(id);

        var result = await handler.HandleAsync(command, cancellationToken);
        
        //Validation
        
        return NoContent();
    }
    
    /// <summary>
    /// Rota para deletar um prato
    /// </summary>
    /// <param name="id"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteDish(string id,
        [FromServices] IHandler<bool, DeleteDishCommand> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        
        //Validators

        await handler.HandleAsync(new DeleteDishCommand(id), cancellationToken);

        return NoContent();
    }
}