namespace NutritionService.PublicFood.ListPublicFoods;

public class ListPublicFoodsQuery
{
    public int Page { get; private set; }
    public int Rows { get; private set; }
    
    public ListPublicFoodsQuery(int? page, int? rows)
    {
        Page = page ?? 1;
        Rows = rows ?? 10;
    }
    
    public ListPublicFoodsQuery()
    {
        
    }
}