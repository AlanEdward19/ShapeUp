using ProfessionalManagementService.Reviews;

namespace ProfessionalManagementService.Clients;

public class ClientDto(Client client)
{
    public string Id { get; private set; } = client.Id;

    public string Email { get; private set; } = client.Email;

    public List<ClientServicePlanDto> ClientServicePlans { get; set; } =
        client.ClientServicePlans.Select(csp => new ClientServicePlanDto(csp)).ToList();

    public List<ClientProfessionalReviewDto> ClientProfessionalReviews { get; set; } =
        client.ClientProfessionalReviews.Select(cpr => new ClientProfessionalReviewDto(cpr)).ToList();
}