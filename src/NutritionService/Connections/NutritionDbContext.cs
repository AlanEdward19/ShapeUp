using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace NutritionService.Connections;

public class NutritionDbContext(IMongoDatabase database)
{
    public IMongoCollection<Food.Food> Foods => database.GetCollection<Food.Food>("Foods");
    public IMongoCollection<Dish.Dish> Dishes => database.GetCollection<Dish.Dish>("Dishes");
    public IMongoCollection<Meal.Meal> Meals => database.GetCollection<Meal.Meal>("Meals");
    public IMongoCollection<DailyMenu.DailyMenu> DailyMenus => database.GetCollection<DailyMenu.DailyMenu>("DailyMenus");
    public IMongoCollection<UserNutrition.UserNutrition> UserNutritions => database.GetCollection<UserNutrition.UserNutrition>("UserNutritions");
}