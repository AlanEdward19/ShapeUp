using NutritionService.Common.Enums;

namespace NutritionService.Meal.CreateMeal;

public class CreateMealCommand
{
    public MealType Type { get; set; }
    public string Name { get; set; }
    public List<Dish.Dish> Dishes { get; set; }
    public List<Food.Food> Foods { get; set; }

    public CreateMealCommand(MealType type, string name, List<Dish.Dish> dishes, List<Food.Food> foods)
    {
        Type = type;
        Name = name;
        Dishes = dishes;
        Foods = foods;
    }

    /// <summary>
    /// Metodo para mapear o comando para a entidade Meal
    /// </summary>
    /// <returns></returns>
    public Meal ToMeal()
    {
        Meal meal = new(Type, Name, Dishes, Foods);

        meal.SetId();

        return meal;
    }
}