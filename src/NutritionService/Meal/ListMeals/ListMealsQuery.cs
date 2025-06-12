namespace NutritionService.Meal.ListMeals;

public class ListMealsQuery
{
    public int Page { get; set; }
    public int Rows { get; set; }

    public ListMealsQuery(int? page, int? rows)
    {
        Page = page ?? 1;
        Rows = rows ?? 10;
    }
    public ListMealsQuery()
    {

    }
}