namespace NutritionService.Dish.GetDishDetails;

public class GetDishDetailsQuery(string id)
{
    public string Id { get; set; } = id;
}