namespace SocialService.Common.Services.BrasilApi;

public class LocationInfoDto
{
    public string Cep { get; set; }
    public string State { get; set; }
    public string City { get; set; }
    public string Neighborhood { get; set; }
    public string Street { get; set; }
    public LocationValueObject Location { get; set; }
}