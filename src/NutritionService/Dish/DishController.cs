using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Filters;

namespace NutritionService.Dish;


[ApiVersion("1.0")]
[ApiController]
[TokenValidatorFilter]
[Route("v{version:apiVersion}/[Controller]")]
public class DishController : ControllerBase
{
    
}