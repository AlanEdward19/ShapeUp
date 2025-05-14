using MongoDB.Driver;
using NutritionService.UserFood;

namespace NutritionService.Connections;

public class NutritionDbContext(IMongoDatabase database)
{
    public IMongoCollection<Food> UserFoods => database.GetCollection<Food>("userFoods");
    public IMongoCollection<Food> PublicFoods => database.GetCollection<Food>("publicFoods");
    public IMongoCollection<Dish.Dish> Dishes => database.GetCollection<Dish.Dish>("dishes");
    public IMongoCollection<Meal.Meal> Meals => database.GetCollection<Meal.Meal>("meals");
    public IMongoCollection<DailyMenu.DailyMenu> DailyMenus => database.GetCollection<DailyMenu.DailyMenu>("dailyMenus");
    public IMongoCollection<UserNutrition.UserNutrition> UserNutritions => database.GetCollection<UserNutrition.UserNutrition>("userNutritions");
}