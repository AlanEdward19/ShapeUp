﻿using MongoDB.Bson.Serialization.Attributes;

namespace NutritionService.Common;

public class SugarDetails(double total, double? addedSugar, double? sugarAlcohols)
{
    [BsonElement("total")]
    public double Total { get; private set; } = total;

    [BsonElement("addedSugar")]
    public double? AddedSugar { get; private set; } = addedSugar;

    [BsonElement("sugarAlcohols")]
    public double? SugarAlcohols { get; private set; } = sugarAlcohols;

    public SugarDetails? Clone()
    {
        return new SugarDetails(Total, AddedSugar, SugarAlcohols);
    }
}