using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using NutritionService.Common;
using NutritionService.Common.Interfaces;
using NutritionService.Food.ApproveFood;
using NutritionService.Food.CreateFood;
using NutritionService.Food.EditFood;
using NutritionService.Food.GetFoodDetails;
using NutritionService.Food.ListUnrevisedFoods;
using SharedKernel.Filters;
using SharedKernel.Utils;

namespace NutritionService.Food;

/// <summary>
///     Controller responsável por gerenciar comidas
/// </summary>
[ApiVersion("1.0")]
[ApiController]
[TokenValidatorFilter]
[Route("v{version:apiVersion}/[Controller]")]
public class FoodController : ControllerBase
{
    /// <summary>
    /// Rota para listar comidas não revisadas
    /// </summary>
    /// <param name="query"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("listUnrevisedFoods")]
    public async Task<IActionResult> ListUnrevisedFoods([FromQuery] ListUnrevisedFoodsQuery query,
        [FromServices] IHandler<IEnumerable<Food>, ListUnrevisedFoodsQuery> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());

        return Ok(await handler.HandleAsync(query, cancellationToken));
    }

    /// <summary>
    /// Rota para pegar detalhes de uma comida
    /// </summary>
    /// <param name="id"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetFoodDetails(string id,
        [FromServices] IHandler<Food, GetFoodDetailsQuery> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());

        return Ok(await handler.HandleAsync(new GetFoodDetailsQuery(id), cancellationToken));
    }

    /// <summary>
    ///     Rota para criar uma comida
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateFood([FromServices] IHandler<Food, CreateFoodCommand> handler,
        [FromBody] CreateFoodCommand command, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());

        return Ok(await handler.HandleAsync(command, cancellationToken));
    }

    /// <summary>
    /// Rota para editar uma comida
    /// </summary>
    /// <param name="id"></param>
    /// <param name="handler"></param>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> EditFood(string id, [FromServices] IHandler<Food, EditFoodCommand> handler,
        [FromBody] EditFoodCommand command, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());

        command.SetId(id);

        return Ok(await handler.HandleAsync(command, cancellationToken));
    }

    /// <summary>
    /// Rota para aprovar uma comida, e marcar como revisada
    /// </summary>
    /// <param name="id"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("approveFood/{id}")]
    public async Task<IActionResult> ApproveFood(string id,
        [FromServices] IHandler<Food, ApproveFoodCommand> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());

        return Ok(await handler.HandleAsync(new ApproveFoodCommand(id), cancellationToken));
    }
}