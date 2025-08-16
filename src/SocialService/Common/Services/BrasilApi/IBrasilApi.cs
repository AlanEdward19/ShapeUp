using Refit;

namespace SocialService.Common.Services.BrasilApi;

public interface IBrasilApi
{
    [Get("/json/{postalCode}")]
    Task<LocationInfoDto> GetLocationInfoByPostalCodeAsync(string postalCode);
}