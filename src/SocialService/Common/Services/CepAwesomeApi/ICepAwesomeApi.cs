using Refit;

namespace SocialService.Common.Services.CepAwesomeApi;

public interface ICepAwesomeApi
{
    [Get("/json/{postalCode}")]
    Task<LocationInfoDto> GetLocationInfoByPostalCodeAsync(string postalCode);
}