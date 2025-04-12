using Microsoft.Extensions.Configuration;

namespace SharedKernel.Utils;

public static class AuthenticationUtils
{
    public static void GetIssuerSigningKey(IConfiguration configuration)
    {
        var httpClient = new HttpClient();
        var response = httpClient.GetStringAsync(configuration["Firebase:IssuerSigning"]).GetAwaiter().GetResult();
        
        var certificates = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(response);

        configuration["Firebase:IssuerSigningKey"] = string.Join("-?-", certificates.Values);
    }
}