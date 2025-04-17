﻿using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using NutritionService.Common;
using NutritionService.Common.Interfaces;
using NutritionService.Meal.CreateMeal;
using NutritionService.Meal.DeleteMeal;
using NutritionService.Meal.EditMeal;
using NutritionService.Meal.GetMealDetails;
using SharedKernel.Filters;
using SharedKernel.Utils;

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
    /// <param name="command"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateMealCommand command,
        [FromServices] IHandler<Meal, CreateMealCommand> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());
        
        var result = await handler.HandleAsync(command, cancellationToken);
        
        //Validations
        
        return Created(HttpContext.Request.Path, await handler.HandleAsync(command, cancellationToken));
    }
    
    /// <summary>
    /// Rota responsável por apagar uma refeição.
    /// </summary>
    /// <param name="mealId"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{mealId}")]
    public async Task<IActionResult> Delete([FromRoute] string mealId,
        [FromServices] IHandler<Meal, DeleteMealCommand> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());
        
        var command = new DeleteMealCommand(mealId);
        
        //Validations
        
        var result = await handler.HandleAsync(command, cancellationToken);
        
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
    public async Task<IActionResult> Put([FromRoute] string mealId,
        [FromBody] EditMealCommand command,
        [FromServices] IHandler<Meal, EditMealCommand> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());
        
        command.SetId(mealId);
        
        //Validations
        
        return Ok(await handler.HandleAsync(command, cancellationToken));
    }
    
    /// <summary>
    /// Rota responsável por buscar uma refeição.
    /// </summary>
    /// <param name="mealId"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{mealId}")]
    public async Task<IActionResult> Get([FromRoute] string mealId,
        [FromServices] IHandler<Meal, GetMealDetailsQuery> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());
        
        var query = new GetMealDetailsQuery(mealId);
        
        //Validations
        
        var result = await handler.HandleAsync(query, cancellationToken);
        
        
        return Ok(result);
    }
}