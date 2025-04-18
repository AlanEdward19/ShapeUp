using MongoDB.Driver;

namespace NutritionService.Connections;

public class NutritionDbContext(IMongoDatabase database)
{
    public IMongoCollection<Food.Food> Foods => database.GetCollection<Food.Food>("foods");
    public IMongoCollection<Dish.Dish> Dishes => database.GetCollection<Dish.Dish>("dishes");
    public IMongoCollection<Meal.Meal> Meals => database.GetCollection<Meal.Meal>("meals");
    public IMongoCollection<DailyMenu.DailyMenu> DailyMenus => database.GetCollection<DailyMenu.DailyMenu>("dailyMenus");
    public IMongoCollection<UserNutrition.UserNutrition> UserNutritions => database.GetCollection<UserNutrition.UserNutrition>("userNutritions");
}