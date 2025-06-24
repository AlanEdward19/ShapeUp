using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using NutritionService.Common;
using NutritionService.Common.Interfaces;
using NutritionService.UserFood.ApproveUserFood;
using NutritionService.UserFood.CreateUserFood;
using NutritionService.UserFood.DeleteUserFood;
using NutritionService.UserFood.EditUserFood;
using NutritionService.UserFood.GetUserFoodByBarCode;
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
    [ProducesResponseType(typeof(IEnumerable<FoodDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListUserFoods([FromQuery] ListFoodsQuery query,
        [FromServices] IHandler<IEnumerable<FoodDto>, ListFoodsQuery> handler,
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
    [ProducesResponseType(typeof(FoodDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserFoodDetails(string id,
        [FromServices] IHandler<FoodDto, GetUserFoodDetailsQuery> handler,
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
    [ProducesResponseType(typeof(FoodDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateUserFood([FromServices] IHandler<FoodDto, CreateUserFoodCommand> handler,
        [FromBody] CreateUserFoodCommand command, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        command.SetCreatedBy(ProfileContext.ProfileId);

        return Created(HttpContext.Request.Path, await handler.HandleAsync(command, cancellationToken));
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
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> EditUserFood(string id, [FromServices] IHandler<bool, EditUserFoodCommand> handler,
        [FromBody] EditUserFoodCommand command, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        command.SetId(id);

        await handler.HandleAsync(command, cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// Rota para inserir comidas públicas na lista de comidas do usuário
    /// </summary>
    /// <param name="handler"></param>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("insertPublicFoods")]
    [ProducesResponseType(typeof(IEnumerable<FoodDto>), StatusCodes.Status201Created)]
    public async Task<IActionResult> InsertPublicFood(
        [FromServices] IHandler<IEnumerable<FoodDto>, InsertPublicFoodsInUserFoodCommand> handler,
        [FromBody] InsertPublicFoodsInUserFoodCommand command, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        
        return Created(HttpContext.Request.Path, await handler.HandleAsync(command, cancellationToken));
    }

    /// <summary>
    /// Rota para aprovar uma comida, e marcar como revisada
    /// </summary>
    /// <param name="id"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("approve/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ApproveUserFood(string id,
        [FromServices] IHandler<bool, ApproveUserFoodCommand> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        await handler.HandleAsync(new ApproveUserFoodCommand(id), cancellationToken);
        return NoContent();
    }
    
    /// <summary>
    /// Rota para deletar uma comida
    /// </summary>
    /// <param name="id"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteUserFood(string id,
        [FromServices] IHandler<bool, DeleteUserFoodCommand> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        var command = new DeleteUserFoodCommand(id);
        await handler.HandleAsync(command, cancellationToken);

        return NoContent();
    }
    
    /// <summary>
    /// Rota para buscar uma comida privada pelo código de barras
    /// </summary>
    /// <param name="query"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("byBarCode")]
    [ProducesResponseType(typeof(FoodDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserFoodByBarCode([FromQuery] GetUserFoodByBarCodeQuery query,
        [FromServices] IHandler<FoodDto, GetUserFoodByBarCodeQuery> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();

        return Ok(await handler.HandleAsync(query, cancellationToken));
    }
}