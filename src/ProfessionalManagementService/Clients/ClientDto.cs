using ProfessionalManagementService.Reviews;

namespace ProfessionalManagementService.Clients;

public class ClientDto(Client client, bool isNutritionist = false, bool isTrainer = false)
{
    public string Id { get; private set; } = client.Id;

    public string Email { get; private set; } = client.Email;
    
    public string Name { get; private set; } = client.Name;

    public bool IsNutritionist { get; private set; } = isNutritionist;
    
    public bool IsTrainer { get; private set; } = isTrainer;

    public List<ClientServicePlanDto> ClientServicePlans { get; set; } =
        client.ClientServicePlans.Select(csp => new ClientServicePlanDto(csp)).ToList();

    public List<ClientProfessionalReviewDto> ClientProfessionalReviews { get; set; } =
        client.ClientProfessionalReviews.Select(cpr => new ClientProfessionalReviewDto(cpr)).ToList();
}