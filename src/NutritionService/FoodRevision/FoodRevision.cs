using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NutritionService.FoodRevision;

public class FoodRevision
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string OriginalFoodId { get; set; }
    public Food.Food RevisedFood { get; set; }
    public string RevisedBy { get; set; }
    public DateTime SubmittedAt { get; set; }
    public string? AdminComment { get; set; }
    public bool? Approved { get; set; }
}
