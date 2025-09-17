namespace NutritionService.PublicFood.ListUsedByUserPublicFoods;

public class ListUsedByUserPublicFoodsQuery
{
    public int Page { get; private set; }
    public int Rows { get; private set; }
    public string UserId { get; private set; } = "";
    
    public ListUsedByUserPublicFoodsQuery(int? page, int? rows)
    {
        Page = page ?? 1;
        Rows = rows ?? 10;
    }
    
    public ListUsedByUserPublicFoodsQuery()
    {
        
    }
    
    public void SetUserId(string id)
    {
        UserId = id;
    }
}