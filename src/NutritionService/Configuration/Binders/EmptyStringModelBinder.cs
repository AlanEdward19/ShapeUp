using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace NutritionService.Configuration.Binders;

public class EmptyStringModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

        if (valueProviderResult == ValueProviderResult.None)
        {
            bindingContext.Result = ModelBindingResult.Success(null);
            return Task.CompletedTask;
        }

        var value = valueProviderResult.FirstValue;
        bindingContext.Result = ModelBindingResult.Success(value ?? string.Empty);
        return Task.CompletedTask;
    }
}