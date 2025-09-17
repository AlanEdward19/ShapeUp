namespace NutritionService.UserFood.ListFoods;

public class ListFoodsQuery
{
    public int Page { get; set; }
    public int Rows { get; set; }
    public string UserId { get; set; } = "";

    public ListFoodsQuery(int? page, int? rows)
    {
        Page = page ?? 1;
        Rows = rows ?? 10;
    }
    public ListFoodsQuery()
    {

    }
    public void SetUserId(string id)
    {
        UserId = id;
    }
}