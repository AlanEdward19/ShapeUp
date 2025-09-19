using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using NutritionService.Common;
using NutritionService.Common.Interfaces;
using NutritionService.Meal.CreateMealForDifferentUser;
using NutritionService.Meal.CreateMealForSameUser;
using NutritionService.Meal.DeleteMeal;
using NutritionService.Meal.EditMeal;
using NutritionService.Meal.GetMealDetails;
using NutritionService.Meal.ListMeals;
using SharedKernel.Filters;
using SharedKernel.Utils;
using ProfileContext = SharedKernel.Utils.ProfileContext;

namespace NutritionService.Meal;


/// <summary>
/// Classe responsável por gerenciar as refeições.
/// </summary>
[ApiVersion("1.0")]
[ApiController]
[TokenValidatorFilter]
[Route("v{version:apiVersion}/[Controller]")]
public class MealController : ControllerBase
{
    /// <summary>
    /// Rota responsável por criar uma refeição.
    /// </summary>
    /// <param name="forSameUserCommand"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(MealDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateMealForSameUser([FromBody] CreateMealForSameUserCommand forSameUserCommand,
        [FromServices] IHandler<MealDto, CreateMealForSameUserCommand> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        
        //Validations
        
        return Created(HttpContext.Request.Path, await handler.HandleAsync(forSameUserCommand, cancellationToken));
    }

    /// <summary>
    /// Rota responsável por criar uma refeição.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="forDifferentUserCommand"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("userId")]
    [ProducesResponseType(typeof(MealDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateMealForDifferentUser(
        string userId,
        [FromBody] CreateMealForDifferentUserCommand forDifferentUserCommand,
        [FromServices] IHandler<MealDto, CreateMealForDifferentUserCommand> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        forDifferentUserCommand.SetUserId(userId);
        
        //Validations
        
        return Created(HttpContext.Request.Path, await handler.HandleAsync(forDifferentUserCommand, cancellationToken));
    }
    
    /// <summary>
    /// Rota responsável por apagar uma refeição.
    /// </summary>
    /// <param name="mealId"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{mealId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete([FromRoute] string mealId,
        [FromServices] IHandler<bool, DeleteMealCommand> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        
        var command = new DeleteMealCommand(mealId);
        
        //Validations
        
        await handler.HandleAsync(command, cancellationToken);
        
        return NoContent();
    }
    
    /// <summary>
    /// Rota responsável por editar uma refeição.
    /// </summary>
    /// <param name="mealId"></param>
    /// <param name="command"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("{mealId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> EditMeal([FromRoute] string mealId,
        [FromBody] EditMealCommand command,
        [FromServices] IHandler<bool, EditMealCommand> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        
        command.SetId(mealId);
        
        //Validations
        await handler.HandleAsync(command, cancellationToken);
        return NoContent();
    }
    
    /// <summary>
    /// Rota responsável por buscar uma refeição.
    /// </summary>
    /// <param name="mealId"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("details/{mealId}")]
    [ProducesResponseType(typeof(MealDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMealDetails([FromRoute] string mealId,
        [FromServices] IHandler<MealDto, GetMealDetailsQuery> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        
        var query = new GetMealDetailsQuery(mealId);
        
        //Validations
        
        var result = await handler.HandleAsync(query, cancellationToken);
        
        
        return Ok(result);
    }

    /// <summary>
    /// Rota responsável por listar as refeições do usuário.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="query"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("list/{userId}")]
    [ProducesResponseType(typeof(IEnumerable<MealDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListMeals(
        string userId,
        [FromQuery] ListMealsQuery query,
        [FromServices] IHandler<IEnumerable<MealDto>, ListMealsQuery> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        query.UserId = userId;
        
        //Validations
        
        return Ok(await handler.HandleAsync(query, cancellationToken));
    }
}