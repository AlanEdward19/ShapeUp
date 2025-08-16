namespace SocialService.Common.Services.BrasilApi;

public class LocationInfoDto
{
    public string Cep { get; set; }
    public string? Lat { get; set; }
    public string? Lng { get; set; }
    public string State { get; set; }
    public string City { get; set; }
    public string Adress { get; set; }

}