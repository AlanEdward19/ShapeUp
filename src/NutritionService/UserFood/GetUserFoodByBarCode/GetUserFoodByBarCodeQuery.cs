namespace NutritionService.UserFood.GetUserFoodByBarCode;

public class GetUserFoodByBarCodeQuery
{
    /// <summary>
    /// Barcode of the food item to retrieve.
    /// </summary>
    public string BarCode { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetUserFoodByBarCodeQuery"/> class.
    /// </summary>
    /// <param name="barCode">The barcode of the food item.</param>
    public GetUserFoodByBarCodeQuery(string barCode)
    {
        BarCode = barCode;
    }
    
    /// <summary>
    /// Default constructor for <see cref="GetUserFoodByBarCodeQuery"/>.
    /// </summary>
    public GetUserFoodByBarCodeQuery(){}
}