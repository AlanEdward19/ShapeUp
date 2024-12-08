using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace SocialService.Configuration;

/// <summary>
///     Classe responsavel por configurar a autenticacao
/// </summary>
public static class Authentication
{
    /// <summary>
    ///     Metodo responsavel por configurar a autenticacao
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureAuthentication(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = configuration["AzureAdB2C:Instance"] + configuration["AzureAdB2C:Domain"] + "/" +
                                    configuration["AzureAdB2C:SignUpSignInPolicyId"] + "/v2.0/";
                options.Audience = configuration["AzureAdB2C:ClientId"];
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["AzureAdB2C:Instance"] + configuration["AzureAdB2C:Domain"] + "/" +
                                  configuration["AzureAdB2C:SignUpSignInPolicyId"] + "/v2.0/",
                    ValidateAudience = true,
                    ValidAudience = configuration["AzureAdB2C:ClientId"],
                    ValidateIssuerSigningKey = true
                };
            });

        return services;
    }
}