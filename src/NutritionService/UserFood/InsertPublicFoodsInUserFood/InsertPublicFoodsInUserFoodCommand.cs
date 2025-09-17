namespace NutritionService.UserFood.InsertPublicFoodsInUserFood;

public class InsertPublicFoodsInUserFoodCommand
{
    public string[] PublicFoodIds { get; set; }
    public string UserId { get; set; } = "";
    
    public void SetUserId(string userId) => UserId = userId;
}