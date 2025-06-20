namespace NutritionService.PublicFood.GetPublicFoodByBarCode;

/// <summary>
/// Represents a query to retrieve public food information by its barcode.
/// </summary>
public class GetPublicFoodByBarCodeQuery
{
    /// <summary>
    /// The barcode of the food item to be retrieved.
    /// </summary>
    public string BarCode { get; set; }

    /// <summary>
    /// Constructor for GetPublicFoodByBarCodeQuery
    /// </summary>
    /// <param name="barCode"></param>
    public GetPublicFoodByBarCodeQuery(string barCode)
    {
        BarCode = barCode;
    }

    /// <summary>
    /// Default constructor for GetPublicFoodByBarCodeQuery
    /// </summary>
    public GetPublicFoodByBarCodeQuery() { }
}