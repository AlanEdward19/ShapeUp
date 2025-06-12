using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using NutritionService.Common;
using NutritionService.Common.Interfaces;
using NutritionService.UserFood.ApproveUserFood;
using NutritionService.UserFood.CreateUserFood;
using NutritionService.UserFood.DeleteUserFood;
using NutritionService.UserFood.EditUserFood;
using NutritionService.UserFood.GetUserFoodDetails;
using NutritionService.UserFood.InsertPublicFoodsInUserFood;
using NutritionService.UserFood.ListFoods;
using SharedKernel.Filters;
using SharedKernel.Utils;
using ProfileContext = SharedKernel.Utils.ProfileContext;

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
    /// Rota para listar comidas
    /// </summary>
    /// <param name="query"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> ListUserFoods([FromQuery] ListFoodsQuery query,
        [FromServices] IHandler<IEnumerable<Food>, ListFoodsQuery> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();

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
    public async Task<IActionResult> GetUserFoodDetails(string id,
        [FromServices] IHandler<Food, GetUserFoodDetailsQuery> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();

        return Ok(await handler.HandleAsync(new GetUserFoodDetailsQuery(id), cancellationToken));
    }

    /// <summary>
    /// Rota para criar uma comida
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateUserFood([FromServices] IHandler<Food, CreateUserFoodCommand> handler,
        [FromBody] CreateUserFoodCommand command, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
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
    public async Task<IActionResult> EditUserFood(string id, [FromServices] IHandler<Food, EditUserFoodCommand> handler,
        [FromBody] EditUserFoodCommand command, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        command.SetId(id);
        

        return Ok(await handler.HandleAsync(command, cancellationToken));
    }

    /// <summary>
    /// Rota para inserir comidas públicas na lista de comidas do usuário
    /// </summary>
    /// <param name="handler"></param>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> InsertPublicFood(
        [FromServices] IHandler<List<Food>, InsertPublicFoodsInUserFoodCommand> handler,
        [FromBody] InsertPublicFoodsInUserFoodCommand command, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        
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
    public async Task<IActionResult> ApproveUserFood(string id,
        [FromServices] IHandler<Food, ApproveUserFoodCommand> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();

        return Ok(await handler.HandleAsync(new ApproveUserFoodCommand(id), cancellationToken));
    }
    
    /// <summary>
    /// Rota para deletar uma comida
    /// </summary>
    /// <param name="id"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserFood(string id,
        [FromServices] IHandler<Food, DeleteUserFoodCommand> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        var command = new DeleteUserFoodCommand(id);
        await handler.HandleAsync(command, cancellationToken);

        return NoContent();
    }
}