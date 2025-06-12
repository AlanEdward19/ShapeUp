namespace NutritionService.PublicFood.ListRevisedPublicFoods;

public class ListRevisedPublicFoodsQuery
{
    public int Page { get; private set; }
    public int Rows { get; private set; }
    
    public ListRevisedPublicFoodsQuery(int? page, int? rows)
    {
        Page = page ?? 1;
        Rows = rows ?? 10;
    }
    
    public ListRevisedPublicFoodsQuery()
    {
        
    }
}