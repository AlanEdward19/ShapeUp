namespace NutritionService.UserNutrition.ListManagedUserNutritions;

public class ListManagedUserNutritionsQuery
{
    public int Page { get; set; }
    public int Rows { get; set; }
    public string NutritionManagerId { get; set; }

    public ListManagedUserNutritionsQuery(int? page, int? rows)
    {
        Page = page ?? 1;
        Rows = rows ?? 10;
    }

    public ListManagedUserNutritionsQuery()
    {
    }
    
    public void SetNutritionManagerId(string managerId) =>  NutritionManagerId = managerId;
}