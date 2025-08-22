using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;

namespace ChatService.Chat;

public class ChatHub(IConfiguration configuration) : Hub
{
    private string GetUserId()
    {
        var requestQueries = Context.GetHttpContext()?.Request.Query;
        string token = requestQueries!.ContainsKey("access_token") ? requestQueries["access_token"].ToString() : "";

        if (string.IsNullOrWhiteSpace(token)) throw new UnauthorizedAccessException("Usuário não está autenticado");
        
        string firebaseIssuer = configuration["Firebase:Issuer"]!;
        string projectId = configuration["Firebase:Credentials:project_id"]!;
        string firebaseIssuerSigningKey = configuration["Firebase:IssuerSigningKey"]!;
        
        if(string.IsNullOrWhiteSpace(token))
            throw new UnauthorizedAccessException("Usuário não está autenticado");
        
        var handler = new JwtSecurityTokenHandler();
        
        var certificates = firebaseIssuerSigningKey.Split("-?-");
        List<SecurityKey> keys = [];
        
        foreach (var cert in certificates)
        {
            var x509Cert = new X509Certificate2(Encoding.UTF8.GetBytes(cert));
            var rsa = x509Cert.GetRSAPublicKey();
            keys.Add(new RsaSecurityKey(rsa));
        }

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = firebaseIssuer,
            ValidateAudience = true,
            ValidAudience = projectId,
            ValidateLifetime = true,
            IssuerSigningKeys = keys
        };

        try
        {
            handler.ValidateToken(token, validationParameters, out _);

            var securityToken  = handler.ReadJwtToken(token);
            
            return securityToken.Claims.FirstOrDefault(c => c.Type == "sub")?.Value!;
        }
        catch(Exception ex)
        {
            throw new UnauthorizedAccessException("Usuário não está autenticado");
        }

        throw new UnauthorizedAccessException("Usuário não está autenticado");
    }
    public override async Task OnConnectedAsync()
    {
        var userId = GetUserId();
        var queryParameters = Context.GetHttpContext()?.Request.Query;
        var profileId = queryParameters["profileId"].ToString();
        bool isProfessionalChat = queryParameters.ContainsKey("isProfessionalChat") && 
            bool.TryParse(queryParameters["isProfessionalChat"].ToString(), out bool isProfessional) && isProfessional;
        
        
        if (userId != null && profileId != null)
        {
            string groupName = GetGroupName(userId, profileId, isProfessionalChat);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }
        
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = GetUserId();
        var queryParameters = Context.GetHttpContext()?.Request.Query;
        var profileId = queryParameters["profileId"].ToString();
        bool isProfessionalChat = queryParameters.ContainsKey("isProfessionalChat") && 
                                  bool.TryParse(queryParameters["isProfessionalChat"].ToString(), out bool isProfessional) && isProfessional;
        
        if (userId != null && profileId != null)
        {
            string groupName = GetGroupName(userId, profileId, isProfessionalChat);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
        
        await base.OnDisconnectedAsync(exception);
    }
    
    private string GetGroupName(string userId1, string userId2, bool isProfessionalChat)
    {
        var orderedIds = new[] { userId1, userId2 }.OrderBy(id => id);
        
        if (isProfessionalChat)
            return $"professional-chat-{string.Join("-", orderedIds)}";
        
        return string.Join("-", orderedIds);
    }
}