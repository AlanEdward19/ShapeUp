using Refit;

namespace SocialService.Common.Services.BrasilApi;

public interface IBrasilApi
{
    [Get("/api/cep/v2/{postalCode}")]
    Task<LocationInfoDto> GetLocationInfoByPostalCodeAsync(string postalCode);
}