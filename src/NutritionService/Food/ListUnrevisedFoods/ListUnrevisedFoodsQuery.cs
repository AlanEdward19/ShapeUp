namespace NutritionService.Food.ListUnrevisedFoods;

public class ListUnrevisedFoodsQuery
{
    public int Page { get; private set; }
    public int Rows { get; private set; }

    public ListUnrevisedFoodsQuery(int? page, int? rows)
    {
        Page = page ?? 1;
        Rows = rows ?? 10;
    }

    public ListUnrevisedFoodsQuery() { }
}