namespace NutritionService.UserNutrition.ListUserNutritions;

public class ListUserNutritionsQuery
{
    public int Page { get; set; }
    public int Rows { get; set; }

    public ListUserNutritionsQuery(int? page, int? rows)
    {
        Page = page ?? 1;
        Rows = rows ?? 10;
    }

    public ListUserNutritionsQuery()
    {
    }
}