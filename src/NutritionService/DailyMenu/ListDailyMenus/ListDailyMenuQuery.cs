using Microsoft.AspNetCore.Mvc;
using NutritionService.Configuration.Binders;

namespace NutritionService.DailyMenu.ListDailyMenus;

public class ListDailyMenuQuery
{
    [ModelBinder(BinderType = typeof(EmptyStringModelBinder))]
    public string? DayOfWeek { get; set; }
    public int Page { get; set; }
    public int Size { get; set; }
    
    public ListDailyMenuQuery(string? dayOfWeek, int? page, int? size)
    {
        DayOfWeek = dayOfWeek;
        Page = page ?? 1;
        Size = size ?? 10;
    }
    public ListDailyMenuQuery() 
    {
    }
}