namespace NutritionService.PublicFood.ListUnrevisedPublicFoods;

public class ListUnrevisedPublicFoodsQuery
{
    public int Page { get; private set; }
    public int Rows { get; private set; }

    public ListUnrevisedPublicFoodsQuery(int? page, int? rows)
    {
        Page = page ?? 1;
        Rows = rows ?? 10;
    }

    public ListUnrevisedPublicFoodsQuery() { }
}