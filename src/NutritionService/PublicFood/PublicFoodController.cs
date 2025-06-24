using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using NutritionService.Common;
using NutritionService.Common.Interfaces;
using NutritionService.PublicFood.ApprovePublicFood;
using NutritionService.PublicFood.CreatePublicFood;
using NutritionService.PublicFood.DeletePublicFood;
using NutritionService.PublicFood.EditPublicFood;
using NutritionService.PublicFood.GetPublicFoodByBarCode;
using NutritionService.PublicFood.GetPublicFoodDetails;
using NutritionService.PublicFood.ListPublicFoods;
using NutritionService.PublicFood.ListRevisedPublicFoods;
using NutritionService.PublicFood.ListUnrevisedPublicFoods;
using NutritionService.UserFood;
using SharedKernel.Filters;
using SharedKernel.Utils;
using ProfileContext = SharedKernel.Utils.ProfileContext;

namespace NutritionService.PublicFood;


/// <summary>
/// Controller responsible for managing public foods
/// </summary>
[ApiVersion("1.0")]
[ApiController]
[TokenValidatorFilter]
[Route("v{version:apiVersion}/[Controller]")]
public class PublicFoodController : ControllerBase
{
    /// <summary>
    /// Rota para listar comidas públicas
    /// </summary>
    /// <param name="query"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<FoodDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPublicFoods([FromQuery] ListPublicFoodsQuery query,
        [FromServices] IHandler<IEnumerable<FoodDto>, ListPublicFoodsQuery> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();

        return Ok(await handler.HandleAsync(query, cancellationToken));
    }
    
    /// <summary>
    /// Rota para obter detalhes de uma comida pública
    /// </summary>
    /// <param name="id"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(FoodDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPublicFoodDetails(string id,
        [FromServices] IHandler<FoodDto, GetPublicFoodDetailsQuery> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();

        return Ok(await handler.HandleAsync(new GetPublicFoodDetailsQuery(id), cancellationToken));
    }
    
    /// <summary>
    /// Rota para listar comidas públicas não revisadas
    /// </summary>
    /// <param name="query"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("listUnrevisedFoods")]
    [ProducesResponseType(typeof(IEnumerable<FoodDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListUnrevisedPublicFoods([FromQuery] ListUnrevisedPublicFoodsQuery query,
        [FromServices] IHandler<IEnumerable<FoodDto>, ListUnrevisedPublicFoodsQuery> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();

        return Ok(await handler.HandleAsync(query, cancellationToken));
    }
    /// <summary>
    /// Rota para listar comidas públicas revisadas
    /// </summary>
    /// <param name="query"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("listRevisedFoods")]
    [ProducesResponseType(typeof(IEnumerable<FoodDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListRevisedPublicFoods([FromQuery] ListRevisedPublicFoodsQuery query,
        [FromServices] IHandler<IEnumerable<FoodDto>, ListRevisedPublicFoodsQuery> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();

        return Ok(await handler.HandleAsync(query, cancellationToken));
    }
    
    /// <summary>
    /// Rota para criar uma comida pública
    /// </summary>
    /// <param name="command"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(FoodDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreatePublicFood([FromBody] CreatePublicFoodCommand command,
        [FromServices] IHandler<FoodDto, CreatePublicFoodCommand> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        command.SetCreatedBy(ProfileContext.ProfileId);
        

        return Created(HttpContext.Request.Path,
            await handler.HandleAsync(command, cancellationToken));
    }
    /// <summary>
    /// Rota para editar uma comida pública
    /// </summary>
    /// <param name="id"></param>
    /// <param name="command"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> EditPublicFood(string id, [FromBody] EditPublicFoodCommand command,
        [FromServices] IHandler<bool, EditPublicFoodCommand> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        command.SetId(id);
        await handler.HandleAsync(command, cancellationToken);
        return NoContent();
    }
    
    /// <summary>
    /// Rota para deletar uma comida pública
    /// </summary>
    /// <param name="id"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeletePublicFood(string id,
        [FromServices] IHandler<bool, DeletePublicFoodCommand> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        var command = new DeletePublicFoodCommand(id);
        
        await handler.HandleAsync(command, cancellationToken);

        return NoContent();
    }
    
    /// <summary>
    /// Rota para aprovar uma comida pública
    /// </summary>
    /// <param name="id"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("approve/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ApprovePublicFood(string id,
        [FromServices] IHandler<bool, ApprovePublicFoodCommand> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        var command = new ApprovePublicFoodCommand(id);
        await handler.HandleAsync(command, cancellationToken);
        return NoContent();
    }
    
    /// <summary>
    /// Rota para obter uma comida pública pelo código de barras
    /// </summary>
    /// <param name="query"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("byBarCode")]
    [ProducesResponseType(typeof(FoodDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPublicFoodByBarCode([FromQuery] GetPublicFoodByBarCodeQuery query,
        [FromServices] IHandler<FoodDto, GetPublicFoodByBarCodeQuery> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();

        return Ok(await handler.HandleAsync(query, cancellationToken));
    }
}