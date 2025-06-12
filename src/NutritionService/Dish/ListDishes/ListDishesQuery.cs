namespace NutritionService.Dish.ListDishes;

public class ListDishesQuery
{
    public int Page { get; set; }
    public int Rows { get; set; }

    public ListDishesQuery(int? page, int? rows)
    {
        Page = page ?? 1;
        Rows = rows ?? 10;
    }
    public ListDishesQuery()
    {

    }
}