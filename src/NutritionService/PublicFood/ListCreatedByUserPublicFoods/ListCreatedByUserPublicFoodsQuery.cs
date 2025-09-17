namespace NutritionService.PublicFood.ListCreatedByUserPublicFoods;

public class ListCreatedByUserPublicFoodsQuery
{
    public int Page { get; private set; }
    public int Rows { get; private set; }
    
    public ListCreatedByUserPublicFoodsQuery(int? page, int? rows)
    {
        Page = page ?? 1;
        Rows = rows ?? 10;
    }
    
    public ListCreatedByUserPublicFoodsQuery()
    {
        
    }
}