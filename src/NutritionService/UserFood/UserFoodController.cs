using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using NutritionService.Common;
using NutritionService.Common.Interfaces;
using NutritionService.UserFood.ApproveUserFood;
using NutritionService.UserFood.CreateUserFood;
using NutritionService.UserFood.EditUserFood;
using NutritionService.UserFood.GetUserFoodDetails;
using NutritionService.UserFood.ListUnrevisedFoods;
using SharedKernel.Filters;
using SharedKernel.Utils;

namespace NutritionService.UserFood;

/// <summary>
///     Controller responsável por gerenciar comidas
/// </summary>
[ApiVersion("1.0")]
[ApiController]
[TokenValidatorFilter]
[Route("v{version:apiVersion}/[Controller]")]
public class UserFoodController : ControllerBase
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
        [FromServices] IHandler<Food, GetUserFoodDetailsQuery> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());

        return Ok(await handler.HandleAsync(new GetUserFoodDetailsQuery(id), cancellationToken));
    }

    /// <summary>
    ///     Rota para criar uma comida
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateFood([FromServices] IHandler<Food, CreateUserFoodCommand> handler,
        [FromBody] CreateUserFoodCommand command, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());
        command.SetCreatedBy(ProfileContext.ProfileId);

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
    public async Task<IActionResult> EditFood(string id, [FromServices] IHandler<Food, EditUserFoodCommand> handler,
        [FromBody] EditUserFoodCommand command, CancellationToken cancellationToken)
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
        [FromServices] IHandler<Food, ApproveUserFoodCommand> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());

        return Ok(await handler.HandleAsync(new ApproveUserFoodCommand(id), cancellationToken));
    }
}