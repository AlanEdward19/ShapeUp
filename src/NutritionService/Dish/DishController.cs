using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using NutritionService.Common;
using NutritionService.Common.Interfaces;
using NutritionService.Dish.CreateDishForDifferentUser;
using NutritionService.Dish.CreateDishForSameUser;
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
    [HttpGet("details/{id}")]
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
    /// <param name="userId"></param>
    /// <param name="query"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("list/{userId}")]
    [ProducesResponseType(typeof(IEnumerable<DishDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListDishes(string userId, [FromQuery] ListDishesQuery query,
        [FromServices] IHandler<IEnumerable<DishDto>, ListDishesQuery> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        query.SetUserId(userId);
        //Validation

        return Ok(await handler.HandleAsync(query, cancellationToken));
    }
    
    /// <summary>
    /// Rota para criar um prato
    /// </summary>
    /// <param name="forSameUserCommand"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DishDto))]
    public async Task<IActionResult> CreateDishForSameUser(CreateDishForSameUserCommand forSameUserCommand,
        [FromServices] IHandler<DishDto, CreateDishForSameUserCommand> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        //Validation
        
        return Created(HttpContext.Request.Path , await handler.HandleAsync(forSameUserCommand, cancellationToken));
    }

    /// <summary>
    /// Rota para criar um prato
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="forDifferentUserCommand"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("userId")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DishDto))]
    public async Task<IActionResult> CreateDishForDifferentUser(
        string userId,
        CreateDishForDifferentUserCommand forDifferentUserCommand,
        [FromServices] IHandler<DishDto, CreateDishForDifferentUserCommand> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        forDifferentUserCommand.SetUserId(userId);
        //Validation
        
        return Created(HttpContext.Request.Path , await handler.HandleAsync(forDifferentUserCommand, cancellationToken));
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